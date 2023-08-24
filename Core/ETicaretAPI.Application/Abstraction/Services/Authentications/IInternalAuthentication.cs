namespace ETicaretAPI.Application.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Dtos.Token> LoginAsync(string userOrEmail, string password, int accessTokenLifeTime);
        Task<Dtos.Token> RefreshTokenLoginAsync(string rsfreshToken);


    }
}
