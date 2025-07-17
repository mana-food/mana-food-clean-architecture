using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.UpdateItem;

public sealed class UpdateItemValidator : AbstractValidator<UpdateItemCommand>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Id).RequiredGuid("ID");
        RuleFor(x => x.Name).ValidName("Nome");
        RuleFor(x => x.Description).ValidDescription();
        RuleFor(x => x.CategoryId).RequiredGuid("Categoria");
    }
}
