﻿using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
    {
        readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
            await _authorizationEndpointService.AssignRoleEndpointAsync(request.Roles, request.AuthorizeMenu, request.Code, request.Type);

            return new();
        }
    }
}
