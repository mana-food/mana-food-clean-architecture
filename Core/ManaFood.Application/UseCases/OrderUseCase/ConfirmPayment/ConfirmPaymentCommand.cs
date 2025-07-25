using MediatR;

namespace ManaFood.Application.UseCases.OrderUseCase.ConfirmPayment
{
    public class ConfirmPaymentCommand : IRequest
    {
        public string PaymentId { get; set; }

        public ConfirmPaymentCommand(string paymentId)
        {
            PaymentId = paymentId;
        }
        public ConfirmPaymentCommand() { }
    }

}