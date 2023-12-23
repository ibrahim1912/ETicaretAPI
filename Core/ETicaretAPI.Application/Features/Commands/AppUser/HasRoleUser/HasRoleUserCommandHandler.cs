using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.HasRoleUser
{
    public class HasRoleUserCommandHandler : IRequestHandler<HasRoleUserCommandRequest, HasRoleUserCommandResponse>
    {
        readonly IUserService _userService;

        public HasRoleUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<HasRoleUserCommandResponse> Handle(HasRoleUserCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await _userService.HasUserRole(request.Token);

            return new()
            {
                State = state,
            };
        }
    }
}
