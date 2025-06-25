using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest<CategoryDto>;