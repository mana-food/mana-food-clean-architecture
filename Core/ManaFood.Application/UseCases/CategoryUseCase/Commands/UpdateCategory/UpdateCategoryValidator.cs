using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;

public sealed class UpdateCategoryValidator : AbstractValidator<UpdateCategoryWithIdCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).RequiredString("Name");
        RuleFor(x => x.Id).RequiredGuid("Id");
    }
}
