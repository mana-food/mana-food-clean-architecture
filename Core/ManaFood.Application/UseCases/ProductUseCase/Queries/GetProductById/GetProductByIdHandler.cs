using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Queries.GetProductById;
public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetBy(p => p.Id == request.Id, cancellationToken);
        return _mapper.Map<ProductDto>(product);
    }
}