using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.CreateCategory;

public sealed record CreateCategoryRequest(string Name) : IRequest<CreateCategoryResponse>;
