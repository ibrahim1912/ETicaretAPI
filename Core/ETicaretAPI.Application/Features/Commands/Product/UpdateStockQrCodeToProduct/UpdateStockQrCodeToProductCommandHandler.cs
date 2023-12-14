using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct
{
    public class UpdateStockQrCodeToProductCommandHandler : IRequestHandler<UpdateStockQrCodeToProductCommandRequest, UpdateStockQrCodeToProductCommandResponse>
    {
        readonly IProductService _productService;

        public UpdateStockQrCodeToProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateStockQrCodeToProductCommandResponse> Handle(UpdateStockQrCodeToProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.UpdateStockToProductAsync(request.ProductId, request.Stock);
            return new();

        }
    }
}
