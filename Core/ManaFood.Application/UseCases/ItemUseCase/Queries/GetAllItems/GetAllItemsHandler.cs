using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Domain.Interfaces;
using MediatR;

namespace ManaFood.Application.UseCases.ItemUseCase.Queries.GetAllItems;
public class GetAllItemsdHandler : IRequestHandler<GetAllItemsQuery, List<ItemDto>>
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public GetAllItemsdHandler(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAll(cancellationToken);
        return _mapper.Map<List<ItemDto>>(items);
    }
}