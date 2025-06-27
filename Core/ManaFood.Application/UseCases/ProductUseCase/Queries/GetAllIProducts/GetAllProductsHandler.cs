using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Queries.GetAllProducts;
public class GetAllProductsdHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductsdHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAll(cancellationToken);
        return _mapper.Map<List<ProductDto>>(products);
    }
}