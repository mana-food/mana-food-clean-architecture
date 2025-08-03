using ManaFood.Application.Interfaces;
using ManaFood.Domain.Enums;
using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, Unit>
{
    private readonly IPaymentStatusService _paymentStatusService;
    private readonly IOrderRepository _orderRepository;

    public ConfirmPaymentHandler(
        IPaymentStatusService paymentStatusService,
        IOrderRepository orderRepository)
    {
        _paymentStatusService = paymentStatusService;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var (status, orderId) = await _paymentStatusService.GetPaymentStatusAsync(request.PaymentId);

            if (status != "approved")
            {
                return Unit.Value;
            }

            var orderGuid = Guid.Parse(orderId);
            var order = await _orderRepository.GetByIdWithProductsAsync(orderGuid);

            if (order == null)
            {
                return Unit.Value;
            }

            order.SetStatus(OrderStatus.RECEIVED);
            await _orderRepository.UpdateAsync(order);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Erro no ConfirmPaymentHandler: {ex.Message}");
        }

        return Unit.Value;
    }

}