using AutoMapper;
using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetByIdProductWithImage
{
    public class GetByIdProductWithImageQueryHandler : IRequestHandler<GetByIdProductWithImageQueryRequest, GetByIdProductWithImageQueryResponse>
    {
        readonly IProductService _productService;
        readonly IMapper _mapper;

        public GetByIdProductWithImageQueryHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<GetByIdProductWithImageQueryResponse> Handle(GetByIdProductWithImageQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _productService.GetByIdProductAsync(request.Id); //servis bagımsız
            GetByIdProductWithImageQueryResponse response = _mapper.Map<GetByIdProductWithImageQueryResponse>(data);
            return response;
        }
    }
}
