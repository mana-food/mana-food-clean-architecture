using MediatR;
using ManaFood.Application.Dtos;
using ManaFood.Application.Shared;

namespace ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery(int Page = 1, int PageSize = 10) : IRequest<PagedResult<CategoryDto>>;
