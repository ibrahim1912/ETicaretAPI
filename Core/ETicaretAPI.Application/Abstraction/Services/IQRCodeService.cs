namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IQRCodeService
    {
        byte[] GenerateQRCode(string text);
    }
}
