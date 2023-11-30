namespace ETicaretAPI.Application.Dtos.Configuration
{
    public class AuthorizeMenu
    {
        public string Name { get; set; }
        public List<AuthorizeAction> AuthorizeActions { get; set; } = new();

    }
}
