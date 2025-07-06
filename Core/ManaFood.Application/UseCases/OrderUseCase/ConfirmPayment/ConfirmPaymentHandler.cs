using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment;

public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, Unit>
{
    public ConfirmPaymentHandler()
    {
        // Inject repositories/services if needed
    }

    public async Task<Unit> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
    {
        // Simulando a confirmação
        Console.WriteLine($"Pagamento confirmado: {request.PaymentId}");
        
        return Unit.Value;
    }
}