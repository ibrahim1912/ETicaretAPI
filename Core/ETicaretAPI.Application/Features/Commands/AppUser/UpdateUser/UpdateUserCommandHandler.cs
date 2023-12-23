using AutoMapper;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.AppUser.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            UpdateUserRequest updateUserRequestMapped = _mapper.Map<UpdateUserRequest>(request);

            UpdateUserResponse response = await _userService.UpdateUserAsync(updateUserRequestMapped);

            UpdateUserCommandResponse updateUserCommandResponse = _mapper.Map<UpdateUserCommandResponse>(response);

            return updateUserCommandResponse;
        }
    }
}
