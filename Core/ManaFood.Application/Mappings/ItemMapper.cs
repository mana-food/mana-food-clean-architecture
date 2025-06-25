using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.ItemUseCase.Commands.CreateItem;
using ManaFood.Application.UseCases.ItemUseCase.Commands.DeleteItem;
using ManaFood.Application.UseCases.ItemUseCase.Commands.UpdateItem;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Mappings;

public sealed class ItemMapper : Profile
{
    public ItemMapper()
    {
        CreateMap<CreateItemCommand, Item>();
        CreateMap<UpdateItemCommand, Item>();
        CreateMap<DeleteItemCommand, Item>();
        CreateMap<Item, ItemDto>();
    }
}
