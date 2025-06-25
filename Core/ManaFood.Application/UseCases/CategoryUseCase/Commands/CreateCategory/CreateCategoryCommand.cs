using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;