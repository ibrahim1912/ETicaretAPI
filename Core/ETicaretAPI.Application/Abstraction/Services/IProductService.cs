namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<byte[]> QrCodeToProductAsync(string productId);

    }
}
