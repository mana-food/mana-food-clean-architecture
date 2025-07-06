using MediatR;

namespace ManaFood.Payment.Application.UseCases.CreatePayment
{
    public class CreatePaymentCommand : IRequest<string>
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}