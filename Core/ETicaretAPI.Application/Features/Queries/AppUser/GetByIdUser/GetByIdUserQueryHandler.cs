using AutoMapper;
using ETicaretAPI.Application.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetByIdUser
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQueryRequest, GetByIdUserQueryResponse>
    {
        readonly IUserService _userService;
        readonly IMapper _mapper;

        public GetByIdUserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _userService.GetByIdUserAsync(request.Token); //servis bagımsız
            GetByIdUserQueryResponse response = _mapper.Map<GetByIdUserQueryResponse>(data);
            return response;
        }
    }
}
