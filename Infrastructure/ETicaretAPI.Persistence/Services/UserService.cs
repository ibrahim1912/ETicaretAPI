using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Helpers;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using U = ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {

        readonly UserManager<U.AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository, IAuthorizationEndpointService authorizationEndpointService)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUserRequest createUserRequest)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUserRequest.UserName,
                Email = createUserRequest.Email,
                NameSurname = createUserRequest.NameSurname,
            }, createUserRequest.Password); ;

            CreateUserResponse response = new()
            {
                Succeeded = result.Succeeded,
            };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";

            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

        public async Task UpdateRefreshTokeAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {

            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task UpdatePasswordAsync(string resetToken, string userId, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user); //db de securitystamp güncelleniyor link aktif olmayacak  
                else
                    throw new PasswordChangeFailedException();

            }

        }
        public async Task<List<ListUser>> GetAllUsersAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                NameSurname = user.NameSurname,
                TwoFactorEnabled = user.TwoFactorEnabled
            }).ToList();
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser user = await _userManager.FindByIdAsync(userIdOrName);
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { }; //buraya hata mesajı da verilebilir
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null)
                return false;

            var hasRole = false;
            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            #region ilk algoritma
            //foreach (var userRole in userRoles) //keşişim olunca break
            //{
            //    if (!hasRole)
            //    {
            //        foreach (var endpointRole in endpointRoles)
            //            if (userRole == endpointRole)
            //            {
            //                hasRole = true;
            //                break;
            //            }
            //    }

            //    else
            //        break;

            //}

            //return hasRole;
            #endregion

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                    {
                        hasRole = true;
                        return hasRole;
                    }

            }

            return hasRole;
        }

        public async Task<bool> HasUserRole(string token)
        {
            if (token == null)
            {
                return false;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            //var userName = jsonToken?.Claims.First().Value.ToString();
            //AppUser? user = await _userManager.FindByNameAsync(userName);

            var nameIdentifierClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            AppUser? user = await _userManager.FindByIdAsync(nameIdentifierClaim);




            string[] defaultRoles = { "Get Basket Items Yetkisi", "Add Item To Basket Yetkisi",
            "Create Orders Yetkisi","Update Quantity Yetkisi","Remove Basket Item Yetkisi"};
            var userRoles = await GetRolesToUserAsync(user.Id);
            bool resultRole = defaultRoles.All(userRoles.Contains) && defaultRoles.Length == userRoles.Length;


            if (user.UserName == Admin.UserName || !resultRole) //admin farklı bir role vermediği sürece paneli göremez
                return true;
            else
                return false;
        }

        public async Task AssignDefaultRoleToUser(string userName, string[] roles)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            await AssignRoleToUserAsync(user.Id, roles);
        }

        public async Task<SingleUser> GetByIdUserAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            //var userName = jsonToken?.Claims.First().Value.ToString();
            //AppUser? user = await _userManager.FindByNameAsync(userName);

            var nameIdentifierClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            AppUser? user = await _userManager.FindByIdAsync(nameIdentifierClaim);

            return new SingleUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NameSurname = user.NameSurname
            };


        }

        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest user)
        {
            AppUser appUser = await _userManager.FindByIdAsync(user.Id);

            appUser.UserName = user.UserName;
            appUser.Email = user.Email;
            appUser.NameSurname = user.NameSurname;
            IdentityResult result = await _userManager.UpdateAsync(appUser);

            UpdateUserResponse response = new()
            {
                Succeeded = result.Succeeded,

            };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla güncelleştirildi.";

            }

            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;


        }

        public int TotalUsersCount => _userManager.Users.Count();
    }
}
