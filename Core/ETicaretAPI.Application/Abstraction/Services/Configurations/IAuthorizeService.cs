using ETicaretAPI.Application.Dtos.Configuration;

namespace ETicaretAPI.Application.Abstraction.Services.Configurations
{
    public interface IAuthorizeService
    {
        List<AuthorizeMenu> GetAuthorizeDefinitionEndpoints(Type assemblyType);
    }
}
