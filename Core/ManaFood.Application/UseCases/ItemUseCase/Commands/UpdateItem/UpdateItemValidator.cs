using FluentValidation;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;

public sealed class UpdateItemValidator : AbstractValidator<CreateItemCommand>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio.");
        RuleFor(x => x.Name).NotNull().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres.");
        RuleFor(x => x.CategoryId).NotNull().WithMessage("Categoria não pode ser nula.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categoria não pode ser vazia.");
    }
}
