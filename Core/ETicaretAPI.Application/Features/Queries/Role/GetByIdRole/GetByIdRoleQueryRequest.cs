using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetByIdRole
{
    public class GetByIdRoleQueryRequest : IRequest<GetByIdRoleQueryResponse>
    {
        public string Id { get; set; }
    }
}