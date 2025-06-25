using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Queries.GetItemById;
public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public GetItemByIdHandler(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetById(request.Id, cancellationToken);
        return _mapper.Map<ItemDto>(item);
    }
}