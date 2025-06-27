using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ProductUseCase.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository repository, IItemRepository itemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetBy(
            p => p.Id == request.Id && !p.Deleted, 
            cancellationToken, 
            p => p.Items
        );
        
        if (product == null)
            throw new ArgumentException($"Produto com ID {request.Id} não encontrado");

        var newItems = await _itemRepository.GetByIds(request.ItemIds, cancellationToken);

        if (newItems.Count() != request.ItemIds.Count)
        {
            var foundIds = newItems.Select(i => i.Id).ToList();
            var notFoundIds = request.ItemIds.Except(foundIds).ToList();
            throw new ArgumentException($"Items não encontrados: {string.Join(", ", notFoundIds)}");
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.UnitPrice = request.UnitPrice;
        product.CategoryId = request.CategoryId;

        product.Items.Clear();

        foreach (var item in newItems)
            product.Items.Add(item);

        await _repository.Update(product, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}
