﻿using ETicaretAPI.Application.Services;
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


            var token = await _authService.GoogleLoginSAync(request.IdToken, 900);
            return new()
            {
                Token = token
            };


        }
    }
}
