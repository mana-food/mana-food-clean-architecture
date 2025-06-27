using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string? Description,
    double UnitPrice,
    Guid CategoryId,
    List<Guid> ItemIds) : IRequest<ProductDto>;