using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQueryRequest, GetAllOrderQueryResponse>
    {
        readonly IOrderService _orderService;

        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _orderService.GetAllOrdersAsync(request.Page, request.Size);



            //return data.Select(o => new GetAllOrderQueryResponse
            //{
            //    CreatedDate = o.CreatedDate,
            //    OrderCode = o.OrderCode,
            //    TotalPrice = o.TotalPrice,
            //    UserName = o.UserName
            //}).ToList();


            return new()
            {
                TotalOrderCount = data.TotalOrderCount,
                Orders = data.Orders,
            };
        }
    }
}
