using MediatR;
using ManaFood.Application.Interfaces;
using ManaFood.Domain.Entities;
using ManaFood.Domain.Enums;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand>
{
    private readonly IPaymentStatusService _paymentStatusService;
    private readonly IOrderRepository _orderRepository;

    public ConfirmPaymentHandler(IPaymentStatusService paymentStatusService, IOrderRepository orderRepository)
    {
        _paymentStatusService = paymentStatusService;
        _orderRepository = orderRepository;
    }

    public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
            (string status, string orderId) = await _paymentStatusService.GetPaymentStatusAsync(request.PaymentId);

        if (!Guid.TryParse(orderId, out var guidOrderId))
            throw new Exception("ID de pedido inválido recebido do Mercado Pago.");

        var order = await _orderRepository.GetBy(x => x.Id == guidOrderId, cancellationToken);

        if (order == null)
            throw new Exception("Pedido não encontrado.");

        order.SetStatus(status switch
        {
            "approved" => OrderStatus.RECEBIDO,
            "rejected" => OrderStatus.CANCELADO,
            _ => OrderStatus.AGUARDANDO_PAGAMENTO
        });

        await _orderRepository.Update(order, cancellationToken);

        return Unit.Value;
    }
}