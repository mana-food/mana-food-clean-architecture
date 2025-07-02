using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductRepository repository, IItemRepository itemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {

        var items = await _itemRepository.GetByIds(request.ItemIds, cancellationToken);

        var product = _mapper.Map<Product>(request);

        product.Items = items;

        await _repository.Create(product, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        var result = _mapper.Map<ProductDto>(product);
        result.ItemIds = product.Items.Select(i => i.Id).ToList();
        return result;
    }
}
