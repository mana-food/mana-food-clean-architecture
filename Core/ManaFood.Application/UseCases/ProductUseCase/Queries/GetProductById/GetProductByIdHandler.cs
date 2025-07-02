using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.Interfaces;
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
        var product = await _repository.GetBy(p => p.Id == request.Id && !p.Deleted, cancellationToken, p => p.Items);
        if (product is null)
            throw new ArgumentException($"Produto com ID {request.Id} não encontrado");
        var result = _mapper.Map<ProductDto>(product);
        result.ItemIds = product.Items.Select(i => i.Id).ToList();
        return result;
    }
}