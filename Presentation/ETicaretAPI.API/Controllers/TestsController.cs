using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Abstraction.Services.Configurations;
using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.ProductImageFilE.GetProductImage;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _configuration;

        readonly IMailService _mailService;
        readonly IMediator _mediator;
        readonly IAuthorizeService _authorizeService;

        public TestsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator, IMailService mailService, IAuthorizeService authorizeService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
            _mailService = mailService;
            _authorizeService = authorizeService;
        }

        [HttpGet("GetProducts")]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() {Id = Guid.NewGuid() , Name="Product 1", Price=100, Stock=10},
                new() {Id = Guid.NewGuid() , Name="Product 2", Price=1430, Stock=13},
                new() {Id = Guid.NewGuid() , Name="Product 3", Price=400, Stock=70},
                new() {Id = Guid.NewGuid() , Name="Product 4", Price=700, Stock=16},
                new() {Id = Guid.NewGuid() , Name="Product 5", Price=800, Stock=6},
            });

            await _productWriteRepository.SaveAsync();

        }
        [HttpGet("GetTracking")]
        public async Task GetTracking()
        {
            //addscope ile hem write hem read için aynı dbcontext kullandığından dolayı aynı instance elde edilir
            Product p = await _productReadRepository.GetByIdAsync("215b6cc9-0817-48a7-bcf9-52ca5214f89b", false);
            p.Name = "Item Tracing No";
            await _productWriteRepository.SaveAsync();

        }

        [HttpGet("GetInterceptorForSaveChangeAsync")]
        public async Task GetInterceptorForSaveChangeAsync()
        {
            var customerId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId, Name = "Customer 1" });

            await _orderWriteRepository.AddAsync(new() { /*CustomerId = customerId*/ Address = "İzmir", Description = "bla bla" });
            await _orderWriteRepository.AddAsync(new() { /*CustomerId = customerId*/ Address = "Ankara", Description = "bla bla 2" });

            await _orderWriteRepository.SaveAsync(); //tek save yetiyor 1 tane instace dan dolayı
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            #region ilk hali
            /*
            public async Task<IActionResult> Get(string id)
            Product product = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
            */
            #endregion

            GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);

        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            #region ilk hali
            /*
            public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate

            }).ToList();

            return Ok(new
            {
                products,
                totalCount,
            });

             */

            #endregion

            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            #region ilk hali
            /*
            public async Task<IActionResult> Post(VM_Create_Product model)
            if (ModelState.IsValid)
            {

            }

            await _productWriteRepository.AddAsync(new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
            */
            #endregion

            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);

            return StatusCode((int)HttpStatusCode.Created);


        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            #region ilk hali
            /*
            public async Task<IActionResult> Put(VM_Update_Product model)
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
            */
            #endregion

            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);

            return Ok();
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {

            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);

            return Ok();
        }


        [HttpPost("[action]")] // ...com/api/products?id=222 query string
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {

            #region upload ilk hali
            /*
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            Random random = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.Name)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }*/
            #endregion

            #region productimagefile örneği
            /*
            var data = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.path
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            */
            #endregion

            #region invoicefile örneği

            /*
            var data = await _fileService.UploadAsync("resource/invoices", Request.Form.Files);

            await _invoiceFileWriteRepository.AddRangeAsync(data.Select(d => new InvoiceFile()
            {
                FileName = d.fileName,
                Path = d.path,
                Price = new Random().Next()
            }).ToList());
            await _invoiceFileWriteRepository.SaveAsync();
            */
            #endregion

            #region file örneği
            /*
            var data = await _fileService.UploadAsync("resource/files", Request.Form.Files);

            await _fileWriteRepository.AddRangeAsync(data.Select(d => new Domain.Entities.File()
            {
                FileName = d.fileName,
                Path = d.path,
            }).ToList());

            await _fileWriteRepository.SaveAsync();
            */
            #endregion

            #region listeler
            /*
            var data = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            var d1 = _fileReadRepository.GetAll(false);
            var d2 = _invoiceFileReadRepository.GetAll(false);
            var d3 = _productImageFileReadRepository.GetAll(false);
            */
            #endregion

            #region storageService file ekleme
            /*
            var data = await _storageService.UploadAsync("resource/photo-images", Request.Form.Files);
            await _productImageFileWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            */
            #endregion

            #region Resim ekleme cqrs öncesi

            /*
             * public async Task<IActionResult> Upload(string id)
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("resource/product-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            
            //foreach (var r in result)
            //{
            //    product.ProductImageFiles.Add(new()
            //    {
            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });
            //}
            

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            */
            #endregion

            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }


        [HttpGet("[action]/{Id}")] // ...com/api/products/222 route data
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            #region ilk hali cqrsden önce
            /*
             
            public async Task<IActionResult> GetProductImages(string id)
            
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));


            return Ok(product.ProductImageFiles.Select(p => new
            {

                Path = $"{_configuration["BaseStorageUrl:CloudinaryUrl"]}/{p.Path}{Path.GetExtension(p.Path)}",
                //p.Path,
                p.FileName,
                p.Id
            }));

            */
            #endregion

            List<GetProductImagesQueryResponse> responses = await _mediator.Send(getProductImagesQueryRequest);

            return Ok(responses);
        }


        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            #region ilk hali cqrsden önce
            /*
             
            public async Task<IActionResult> DeleteProductImage(string id, string imageId)
             
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                  .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImageFiles.Remove(productImageFile);

            await _productWriteRepository.SaveAsync();

            */
            #endregion

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);

            return Ok();
        }

        #region Mail gönderen endpoint
        //[HttpGet]
        //public async Task<IActionResult> ExampleMailTest()
        //{
        //    string? config = _configuration["Mail:Receiver"];
        //    await _mailService.SendMailAsync(config, "Örnek Mail",
        //       "<strong>Bu bir örnek maildir.</strong>");

        //    return Ok();
        //}
        #endregion



    }
}

