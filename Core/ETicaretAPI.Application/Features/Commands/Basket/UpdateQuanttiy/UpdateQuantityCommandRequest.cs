using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Basket.UpdateQuanttiy
{
    public class UpdateQuantityCommandRequest : IRequest<UpdateQuantityCommandResponse>
    {
        public string BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}