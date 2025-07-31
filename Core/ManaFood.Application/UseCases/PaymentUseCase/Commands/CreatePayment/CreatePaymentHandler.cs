using ManaFood.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, string>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"📦 Criando pagamento para: {request.OrderId}");
            Console.WriteLine($"👤 Payer: {request.PayerFirstName} {request.PayerLastName} | Email: {request.PayerEmail}");

            var response = await _paymentService.CreatePaymentAsync(
                request.OrderId,
                request.TotalAmount,
                request.PayerEmail,
                request.PayerFirstName,
                request.PayerLastName,
                request.PayerId
            );

            Console.WriteLine($"✅ Pagamento criado com sucesso! Resposta: {response}");

            return response;
        }
    }
}