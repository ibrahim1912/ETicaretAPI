using ETicaretAPI.Application.Services.Authentications;

namespace ETicaretAPI.Application.Services
{
    public interface IAuthService : IExternalAuthenticaiton, IInternalAuthentication
    {

    }
}
