using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
    {
        readonly IRoleService _roleService;
        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var (datas, count) = _roleService.GetAllRoles(request.Page, request.Size);
            return new()
            {
                Datas = datas,
                TotalCount = count
            };
        }
    }
}
