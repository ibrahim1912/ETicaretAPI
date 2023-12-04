namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleEndpointAsync(string[] roles, string authorizeMenu, string code, Type type);

        public Task<List<string>?> GetRolesToEndpointAsync(string code, string authorizeMenu);
    }
}
