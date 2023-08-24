using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int second, AppUser appUser);
        string CreateRefreshToken();
    }
}
