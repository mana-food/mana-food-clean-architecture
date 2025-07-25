using ManaFood.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, string>
    {
        private readonly IPaymentService _paymentService;
        private readonly IValidator<CreatePaymentCommand> _validator;

        public CreatePaymentHandler(IPaymentService paymentService, IValidator<CreatePaymentCommand> validator)
        {
            _paymentService = paymentService;
            _validator = validator;
        }

        public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Erro de validação: {errors}");
            }

            var paymentId = await _paymentService.CreatePaymentAsync(request.OrderId, request.Amount);

            return paymentId;
        }
    }
}