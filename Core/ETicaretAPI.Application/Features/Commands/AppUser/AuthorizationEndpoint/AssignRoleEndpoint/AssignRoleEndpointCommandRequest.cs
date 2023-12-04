using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.AuthorizationEndpoint.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string Code { get; set; }
        public string AuthorizeMenu { get; set; }
        public Type? Type { get; set; }
    }
}