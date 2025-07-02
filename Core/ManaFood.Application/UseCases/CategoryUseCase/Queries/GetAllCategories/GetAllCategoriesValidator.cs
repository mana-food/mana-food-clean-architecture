using FluentValidation;
using ManaFood.Application.Shared;
using ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.GetAllCategoriesCategory;

public sealed class GetAllCategoriesCategoryValidator : AbstractValidator<GetAllCategoriesQuery>
{
    public GetAllCategoriesCategoryValidator()
    {
        RuleFor(x => x.Page).RequiredPagination("Page");
        RuleFor(x => x.PageSize).RequiredPagination("PageSize");
    }
}
