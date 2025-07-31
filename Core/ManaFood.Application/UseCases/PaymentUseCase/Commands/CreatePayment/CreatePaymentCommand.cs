using MediatR;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentCommand : IRequest<string>
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }

        public required string PayerEmail { get; set; }
        public required string PayerFirstName { get; set; }
        public required string PayerLastName { get; set; }
        public required string PayerId { get; set; }
    }

}
