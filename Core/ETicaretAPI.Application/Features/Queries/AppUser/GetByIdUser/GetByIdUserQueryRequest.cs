using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetByIdUser
{
    public class GetByIdUserQueryRequest : IRequest<GetByIdUserQueryResponse>
    {
        public string Token { get; set; }
    }
}