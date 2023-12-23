using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.HasRoleUser
{
    public class HasRoleUserCommandRequest : IRequest<HasRoleUserCommandResponse>
    {
        public string Token { get; set; }
    }
}