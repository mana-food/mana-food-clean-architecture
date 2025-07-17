using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;

public sealed class CreateItemValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Description).ValidDescription();
        RuleFor(x => x.CategoryId).RequiredGuid("Categoria");
    }
}
