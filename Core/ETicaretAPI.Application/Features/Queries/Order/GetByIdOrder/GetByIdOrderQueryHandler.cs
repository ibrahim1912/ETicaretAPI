using AutoMapper;
using ETicaretAPI.Application.Abstraction.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Order.GetByIdOrder
{
    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQueryRequest, GetByIdOrderQueryResponse>
    {
        readonly IOrderService _orderService;
        readonly IMapper _mapper;

        public GetByIdOrderQueryHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<GetByIdOrderQueryResponse> Handle(GetByIdOrderQueryRequest request, CancellationToken cancellationToken)
        {
            #region Mapper olmadan
            var data = await _orderService.GetByIdOrderAsync(request.Id); //servis bagımsız
            return new() // biz cqrs e döndürüyoruz
            {
                Id = data.Id,
                Address = data.Address,
                BasketItems = data.BasketItems,
                CreatedDate = data.CreatedDate,
                Description = data.Description,
                OrderCode = data.OrderCode
            };
            #endregion

            #region Mapper ile

            //var data = await _orderService.GetByIdOrderAsync(request.Id); //servis bagımsız
            //GetByIdOrderQueryResponse response = _mapper.Map<GetByIdOrderQueryResponse>(data);
            //return response;

            #endregion


        }
    }
}
