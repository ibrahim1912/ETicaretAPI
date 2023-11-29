using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Dtos.Order;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            (bool succeeded, CompletedOrderDto dto) = await _orderService.CompleteOrderAsync(request.Id);

            #region Alışveriş Tamamlanınca Mail Atan Kod Çalışıyor
            //if (succeeded)
            //    await _mailService.SendCompletedOrderMailAsync(dto.Email, dto.OrderCode, dto.OrderDate, dto.UserSurname, dto.UserSurname);
            #endregion

            return new();
        }
    }
}
