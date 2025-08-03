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
        Console.WriteLine("📬 Webhook recebido!");
        Console.WriteLine($"🆔 PaymentId (merchant_order_id): {request.PaymentId}");

        try
        {
            // 1. Buscar status do pagamento e ID do pedido (external_reference)
            var (status, orderId) = await _paymentStatusService.GetPaymentStatusAsync(request.PaymentId);

            Console.WriteLine($"🔍 Status do pagamento: {status}");
            Console.WriteLine($"🔗 Pedido correspondente: {orderId}");

            // 2. Verificar se pagamento foi fechado
            if (status != "closed")
            {
                Console.WriteLine("⚠️ Pagamento ainda não finalizado.");
                return Unit.Value;
            }

            // 3. Buscar pedido e atualizar status
            var orderGuid = Guid.Parse(orderId);
            var order = await _orderRepository.GetByIdWithProductsAsync(orderGuid);

            if (order == null)
            {
                Console.WriteLine("❌ Pedido não encontrado.");
                return Unit.Value;
            }

            order.SetStatus(OrderStatus.RECEIVED);
            await _orderRepository.UpdateAsync(order);

            Console.WriteLine("✅ Pedido atualizado para RECEIVED, mediante pagamento identificado!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("💥 Erro ao processar o webhook:");
            Console.WriteLine(ex.Message);
        }

        return Unit.Value;
    }
}