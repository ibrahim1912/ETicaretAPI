using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Consts;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateStockQrCodeToProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : BaseController
    {
        readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {

            _productService = productService;
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

            GetByIdProductQueryResponse response = await Mediator.Send(getByIdProductQueryRequest);
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

            //return Unauthorized();
            // throw new Exception("Lay loy lom");
            GetAllProductQueryResponse response = await Mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
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

            CreateProductCommandResponse response = await Mediator.Send(createProductCommandRequest);

            return StatusCode((int)HttpStatusCode.Created);


        }


        [HttpPut("update-product")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
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

            UpdateProductCommandResponse response = await Mediator.Send(updateProductCommandRequest);

            return Ok();
        }


        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Remove Product")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {

            RemoveProductCommandResponse response = await Mediator.Send(removeProductCommandRequest);

            return Ok();
        }


        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product File")]
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
            UploadProductImageCommandResponse response = await Mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }



        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Products Images")]
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

            List<GetProductImagesQueryResponse> responses = await Mediator.Send(getProductImagesQueryRequest);

            return Ok(responses);
        }


        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product Image")]
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
            RemoveProductImageCommandResponse response = await Mediator.Send(removeProductImageCommandRequest);

            return Ok();
        }


        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change Showcase Image")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            ChangeShowcaseImageCommandResponse response = await Mediator.Send(changeShowcaseImageCommandRequest);

            return Ok(response);
        }



        [HttpGet("qrcode/{productId}")]
        public async Task<IActionResult> GetQrCodeToProduct([FromRoute] string productId)
        {

            var response = await _productService.QrCodeToProductAsync(productId);
            return File(response, "image/png");
        }


        [HttpPut("qrcode")]
        public async Task<IActionResult> UpdateStockQrCodeToProduct(UpdateStockQrCodeToProductCommandRequest updateStockQrCodeToProductCommandRequest)
        {
            UpdateStockQrCodeToProductCommandResponse response = await Mediator.Send(updateStockQrCodeToProductCommandRequest);
            return Ok(response);
        }
    }
}

