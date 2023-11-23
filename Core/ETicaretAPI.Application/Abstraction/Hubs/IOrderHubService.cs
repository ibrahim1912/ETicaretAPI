namespace ETicaretAPI.Application.Abstraction.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(string message);
    }
}
