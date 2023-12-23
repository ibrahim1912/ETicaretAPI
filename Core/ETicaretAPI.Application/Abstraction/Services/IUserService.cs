using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUserRequest createUserRequest);
        Task UpdateRefreshTokeAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUsersAsync(int page, int size);
        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code); //bu sayfa için rol yetkisi var mı

        Task<bool> HasUserRole(string token);
        Task AssignDefaultRoleToUser(string userName, string[] roles);
        Task<SingleUser> GetByIdUserAsync(string token);

        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest user);
        int TotalUsersCount { get; }
    }
}
