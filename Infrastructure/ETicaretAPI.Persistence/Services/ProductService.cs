using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using System.Text.Json;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductService : IProductService
    {

        #region Sonra bak
        //public List<Product> GetProducts()
        //    => new()
        //    {
        //        new() {Id = Guid.NewGuid() , Name="Product 1", Price=100, Stock=10},
        //        new() {Id = Guid.NewGuid() , Name="Product 2", Price=1430, Stock=13},
        //        new() {Id = Guid.NewGuid() , Name="Product 3", Price=400, Stock=70},
        //        new() {Id = Guid.NewGuid() , Name="Product 4", Price=700, Stock=16},
        //        new() {Id = Guid.NewGuid() , Name="Product 5", Price=800, Stock=6},
        //    };
        #endregion

        readonly IProductReadRepository _productReadRepository;
        readonly IQRCodeService _qrCodeService;
        readonly IProductWriteRepository _productWriteRepository;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<byte[]> QrCodeToProductAsync(string productId)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var plainObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CreatedDate
            };

            string plainText = JsonSerializer.Serialize(plainObject);

            return _qrCodeService.GenerateQRCode(plainText);
        }

        public async Task UpdateStockToProductAsync(string productId, int stock)
        {
            Product product = await _productReadRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found");

            product.Stock = stock;
            await _productWriteRepository.SaveAsync();

        }
    }
}
