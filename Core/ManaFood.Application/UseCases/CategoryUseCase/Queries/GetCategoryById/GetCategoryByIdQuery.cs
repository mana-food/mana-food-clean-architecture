using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.CategoryUseCase.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
