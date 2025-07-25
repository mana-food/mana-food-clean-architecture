using FluentValidation;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");
        }
    }
}
