using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Commands.UpdateItem;

public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateItemHandler(IItemRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ItemDto> Handle(UpdateItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _repository.GetBy(i => i.Id == request.Id && !i.Deleted, cancellationToken);
        
        if (item == null)
            throw new ArgumentException($"Item com ID {request.Id} não encontrado");


        item.Name = request.Name;
        item.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(item, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<ItemDto>(item);
    }
}
