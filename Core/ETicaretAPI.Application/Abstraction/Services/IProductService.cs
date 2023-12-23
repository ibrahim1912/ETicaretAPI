using ETicaretAPI.Application.Dtos.Product;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<byte[]> QrCodeToProductAsync(string productId);
        Task UpdateStockToProductAsync(string productId, int stock);

        Task<SingleProduct> GetByIdProductAsync(string id);
        Task ChangeShowcaseImageAsync(string productId, string imageId);
        Task Test(string productId);

    }
}
