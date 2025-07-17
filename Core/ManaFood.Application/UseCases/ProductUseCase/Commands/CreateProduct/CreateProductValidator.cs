using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;

public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Description).ValidDescription();
        RuleFor(x => x.UnitPrice).ValidPrice("Preço unitário");
        RuleFor(x => x.CategoryId).RequiredGuid("Categoria");
        RuleFor(x => x.ItemIds).NotEmptyList("Itens");
        
        RuleForEach(x => x.ItemIds)
            .RequiredGuid("Item ID");
    }
}
