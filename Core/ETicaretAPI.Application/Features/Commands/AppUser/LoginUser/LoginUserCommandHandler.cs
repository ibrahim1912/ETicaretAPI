using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;


        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;

        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            #region ilk hali
            /*
            U.AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);

            if (user == null)

                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)//authentication başarılı
            {
                Token token = _tokenHandler.CreateAccessToken(5);

                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }

            throw new AuthenticationErrorException();
            */
            #endregion


            var token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 15);

            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };



        }
    }



}
