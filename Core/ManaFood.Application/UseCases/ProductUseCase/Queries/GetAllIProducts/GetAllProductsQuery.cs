using MediatR;
using ManaFood.Application.Dtos;

namespace ManaFood.Application.UseCases.ProductUseCase.Queries.GetAllProducts;

public sealed record GetAllProductsQuery : IRequest<List<ProductDto>>;
