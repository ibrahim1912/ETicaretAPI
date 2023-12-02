using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Role.GetAllRoles
{
    public class GetAllRolesQueryRequest : IRequest<GetAllRolesQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}