using ETicaretAPI.Application.Dtos.Order;

namespace ETicaretAPI.Application.Abstraction.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);

        Task<ListOrder> GetAllOrdersAsync(int page, int size);

        Task<SingleOrder> GetByIdOrderAsync(string id);
    }
}
