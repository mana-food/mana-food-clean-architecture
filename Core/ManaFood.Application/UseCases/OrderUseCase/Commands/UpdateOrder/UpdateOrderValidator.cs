using FluentValidation;

namespace ManaFood.Application.UseCases.OrderUseCase.Commands.UpdateOrder;

public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.OrderStatus).InclusiveBetween(0, 4).WithMessage("Estado do pedido deve ser entre 0 e 5.");
    }
}
