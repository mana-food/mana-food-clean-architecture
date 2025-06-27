using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ProductUseCase.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
