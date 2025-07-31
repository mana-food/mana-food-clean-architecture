using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, Unit>
{
    public ConfirmPaymentHandler()
    {
    }

   public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
{
    Console.WriteLine("Webhook recebido!");
    Console.WriteLine($"PaymentId: {request.PaymentId}");

    return Unit.Value;
}

}