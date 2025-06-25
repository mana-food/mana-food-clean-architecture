using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.CategoryUseCase.Queries.GetAllCategories;

public sealed record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;
