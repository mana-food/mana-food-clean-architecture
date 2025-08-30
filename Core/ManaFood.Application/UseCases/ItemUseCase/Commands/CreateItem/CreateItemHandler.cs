using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Entities;
using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;

public class CreateItemHandler : IRequestHandler<CreateItemCommand, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateItemHandler(IItemRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper     = mapper;
    }

    public async Task<ItemDto> Handle(CreateItemCommand request,
        CancellationToken cancellationToken)
    {

        var item    = _mapper.Map<Item>(request);

        await _repository.Create(item, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<ItemDto>(item);
    }
}