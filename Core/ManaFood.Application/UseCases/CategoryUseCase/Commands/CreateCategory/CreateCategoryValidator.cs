using FluentValidation;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;

public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).RequiredString("Name");
    }
}
