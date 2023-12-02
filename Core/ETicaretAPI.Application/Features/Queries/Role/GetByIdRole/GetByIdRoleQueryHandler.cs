using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetByIdRole
{
    public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQueryRequest, GetByIdRoleQueryResponse>
    {
        readonly IRoleService _roleService;
        public GetByIdRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<GetByIdRoleQueryResponse> Handle(GetByIdRoleQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _roleService.GetByIdRole(request.Id);
            return new()
            {
                Id = data.id,
                Name = data.name,
            };
        }
    }
}
