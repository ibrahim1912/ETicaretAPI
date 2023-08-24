using AutoMapper;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            #region ilk hali
            /*
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname,
            }, request.Password); ;

            CreateUserCommandResponse response = new()
            {
                Succeeded = result.Succeeded,
            };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";

            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
            */
            #endregion

            #region mapper olmadan 
            /*
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                UserName = request.UserName
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };
            */
            #endregion

            #region mapper deneme

            CreateUserRequest createUserRequestMapped = _mapper.Map<CreateUserRequest>(request);
            CreateUserResponse response = await _userService.CreateAsync(createUserRequestMapped);
            CreateUserCommandResponse createUserCommandResponse = _mapper.Map<CreateUserCommandResponse>(response);

            return createUserCommandResponse;

            #endregion








        }
    }
}
