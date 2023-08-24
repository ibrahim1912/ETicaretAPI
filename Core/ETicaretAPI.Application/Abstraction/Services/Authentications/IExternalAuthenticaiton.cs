namespace ETicaretAPI.Application.Services.Authentications
{
    public interface IExternalAuthenticaiton
    {

        Task<Dtos.Token> GoogleLoginSAync(string idToken, int accessTokenLifeTime);
        Task<Dtos.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
    }
}
