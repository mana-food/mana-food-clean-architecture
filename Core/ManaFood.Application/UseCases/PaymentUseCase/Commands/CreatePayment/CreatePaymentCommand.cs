using MediatR;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<string>
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }

        public CreatePaymentCommand(Guid orderId, decimal amount)
        {
            OrderId = orderId;
            Amount = amount;
        }

        public CreatePaymentCommand() { }
    }
}
