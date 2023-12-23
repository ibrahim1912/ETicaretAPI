using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProductWithImage
{
    public class GetByIdProductWithImageQueryRequest : IRequest<GetByIdProductWithImageQueryResponse>
    {
        public string Id { get; set; }
    }
}