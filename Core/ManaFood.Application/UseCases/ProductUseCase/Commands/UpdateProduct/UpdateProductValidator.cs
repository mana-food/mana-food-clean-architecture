using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.UpdateProduct;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Id).RequiredGuid("ID");
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Description).ValidDescription();
        RuleFor(x => x.UnitPrice).ValidPrice("Preço unitário");
        RuleFor(x => x.CategoryId).RequiredGuid("Categoria");
        RuleFor(x => x.ItemIds).NotEmptyList("Itens");
        
        RuleForEach(x => x.ItemIds)
            .RequiredGuid("Item ID");
    }
}
