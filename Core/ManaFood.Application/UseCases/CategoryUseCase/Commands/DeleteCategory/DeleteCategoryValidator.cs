using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;

public sealed class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id).RequiredGuid("Id");
    }
}
