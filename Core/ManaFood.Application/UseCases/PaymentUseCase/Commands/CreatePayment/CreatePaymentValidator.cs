using FluentValidation;

namespace ManaFood.Application.UseCases.PaymentUseCase.Commands.CreatePayment
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEmpty().WithMessage("O ID do pedido é obrigatório.");

            RuleFor(p => p.TotalAmount)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");

            RuleFor(p => p.PayerEmail)
                .NotEmpty().WithMessage("O e-mail do pagador é obrigatório.")
                .EmailAddress().WithMessage("O e-mail do pagador é inválido.");

            RuleFor(p => p.PayerFirstName)
                .NotEmpty().WithMessage("O nome do pagador é obrigatório.");

            RuleFor(p => p.PayerLastName)
                .NotEmpty().WithMessage("O sobrenome do pagador é obrigatório.");

            RuleFor(p => p.PayerId)
                .NotEmpty().WithMessage("O documento do pagador é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve conter exatamente 11 dígitos numéricos.");
        }
    }
}