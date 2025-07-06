using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

public class ConfirmPaymentCommand : IRequest<Unit>
{
    public string PaymentId { get; }

    public ConfirmPaymentCommand(string paymentId)
    {
        PaymentId = paymentId;
    }
}