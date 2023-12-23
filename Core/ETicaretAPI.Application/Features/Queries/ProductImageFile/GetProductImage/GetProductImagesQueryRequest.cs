using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFilE.GetProductImage
{
    public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
