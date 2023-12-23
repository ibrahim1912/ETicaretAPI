using ETicaretAPI.Application.Services.Authentications;

namespace ETicaretAPI.Application.Services
{
    public interface IAuthService : IExternalAuthenticaiton, IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);

    }
}
