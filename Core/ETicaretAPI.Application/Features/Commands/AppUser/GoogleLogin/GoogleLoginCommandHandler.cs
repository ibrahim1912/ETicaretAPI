using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;


        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;

        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            #region ilk hali
            /*
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "667495971238-pkc9orruq7sgutqlq8ms6ehvvbovntkc.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            U.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name
                    };

                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins
            else
                throw new Exception("Invalid external authentication");

            Token token = _tokenHandler.CreateAccessToken(5);

            return new()
            {
                Token = token
            };
            */
            #endregion

            var token = await _authService.GoogleLoginSAync(request.IdToken, 900);
            return new()
            {
                Token = token
            };


        }
    }
}
