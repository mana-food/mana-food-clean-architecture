using FluentValidation;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;

public sealed class UpdateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome não pode ser vazio.");
        RuleFor(x => x.Name).NotNull().WithMessage("Nome não pode ser nulo.");
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Nome precisa ter no mínimo 3 caracteres.");
    }
}
