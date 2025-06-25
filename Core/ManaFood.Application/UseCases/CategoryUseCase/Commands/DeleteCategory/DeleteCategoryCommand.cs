using MediatR;

namespace ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;