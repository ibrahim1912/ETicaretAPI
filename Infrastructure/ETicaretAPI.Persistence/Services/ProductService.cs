using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ProductService(IProductReadRepository productReadRepository, IQRCodeService qrCodeService, IProductWriteRepository productWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _qrCodeService = qrCodeService;
            _productWriteRepository = productWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
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



        public async Task<SingleProduct> GetByIdProductAsync(string id)
        {
            var data = _productReadRepository.Table
                .Include(P => P.ProductImageFiles)
            .FirstOrDefault(p => p.Id == Guid.Parse(id));

            //data.ProductImageFiles.Where(p=>p.Showcase == true)

            return new()
            {

                Id = data.Id.ToString(),
                Name = data.Name,
                Stock = data.Stock,
                Price = data.Price,
                ProductImageFiles = data.ProductImageFiles.OrderByDescending(p => p.CreatedDate).Select(pi => new ProductImageFileDto
                {
                    Id = pi.Id.ToString(),
                    Path = pi.Path,
                    FileName = pi.FileName,
                    Showcase = pi.Showcase
                }).ToList(),
            };
        }


        public async Task ChangeShowcaseImageAsync(string productId, string imageId)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products)
             .SelectMany(p => p.Products, (pif, p) => new
             {
                 pif,
                 p
             });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(productId) && p.pif.Showcase);
            if (data != null)
                data.pif.Showcase = false;

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(imageId));

            if (image != null)
                image.pif.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();


        }

        public async Task Test(string productId)
        {


            var product = await GetByIdProductAsync(productId);

            var productImageFileId = product.ProductImageFiles.FirstOrDefault().Id;
            if (productImageFileId != null)
                await ChangeShowcaseImageAsync(productId, productImageFileId);
        }

    }
}


