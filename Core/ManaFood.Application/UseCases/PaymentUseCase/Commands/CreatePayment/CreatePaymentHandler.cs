using ManaFood.Application.Interfaces;
using ManaFood.Application.Dtos;
using MediatR;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentResponse>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<CreatePaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await _paymentService.CreatePaymentAsync(request.OrderId);
        }
    }

}