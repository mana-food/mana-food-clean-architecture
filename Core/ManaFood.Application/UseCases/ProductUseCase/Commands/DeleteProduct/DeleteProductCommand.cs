using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Unit>;