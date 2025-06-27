using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string? Description,
    Guid CategoryId,
    double UnitPrice,
    List<Guid> ItemIds) : IRequest<ProductDto>;