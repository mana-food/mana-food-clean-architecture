using FluentValidation;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;

public sealed class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio.");
        RuleFor(x => x.Name).NotNull().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres.");
    }
}
