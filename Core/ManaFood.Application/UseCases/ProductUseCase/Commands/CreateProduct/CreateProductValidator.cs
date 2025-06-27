using FluentValidation;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;

public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio.");
        RuleFor(x => x.Name).NotNull().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres.");
        RuleFor(x => x.CategoryId).NotNull().WithMessage("Categoria não pode ser nula.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categoria não pode ser vazia.");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");
        RuleFor(x => x.ItemIds).NotEmpty().WithMessage("Lista de itens não pode ser vazia.");
        RuleFor(x => x.ItemIds).NotNull().WithMessage("Lista de itens não pode ser nula.");
        RuleFor(x => x.ItemIds).Must(items => items.Count > 0).WithMessage("Lista de itens deve conter pelo menos um item.");
    }
}
