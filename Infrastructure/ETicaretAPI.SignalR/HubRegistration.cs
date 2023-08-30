using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ETicaretAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication app)
        {
            app.MapHub<ProductHub>("/products-hub");
        }
    }
}
