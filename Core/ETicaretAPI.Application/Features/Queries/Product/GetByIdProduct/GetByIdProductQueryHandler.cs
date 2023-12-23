using ETicaretAPI.Application.Repositories;
using MediatR;


namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
{
    readonly IProductReadRepository _productReadRepository;


    public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;

    }

    public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
    {

        var product = await _productReadRepository.GetByIdAsync(request.Id, false);

        return new()
        {
            Id = product.Id.ToString(),
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,

        };





    }
}

