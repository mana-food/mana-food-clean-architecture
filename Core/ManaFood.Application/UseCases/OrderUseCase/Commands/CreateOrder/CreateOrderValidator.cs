using FluentValidation;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.CreateOrder;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.PaymentMethod).InclusiveBetween(0, 2).WithMessage("Método de pagamento deve ser entre 0 e 2.");
        RuleFor(x => x.Products).NotEmpty().WithMessage("Lista de produtos não pode estar vazia");
    }
}
