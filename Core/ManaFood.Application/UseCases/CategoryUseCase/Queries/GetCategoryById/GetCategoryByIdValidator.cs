using FluentValidation;
using ManaFood.Application.Shared;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetCategoryById;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.GetCategoryByIdCategory;

public sealed class GetCategoryByIdCategoryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdCategoryValidator()
    {
        RuleFor(x => x.Id).RequiredGuid("Id");
    }
}
