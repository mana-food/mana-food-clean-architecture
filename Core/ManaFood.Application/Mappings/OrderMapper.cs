using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.OrderUseCase.Commands.CreateOrder;
using ManaFood.Application.UseCases.OrderUseCase.Commands.DeleteOrder;
using ManaFood.Application.UseCases.OrderUseCase.Commands.UpdateOrder;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Mappings;

public sealed class OrderMapper : Profile
{
    public OrderMapper()
    {
        CreateMap<CreateOrderCommand, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore()); 
        CreateMap<UpdateOrderCommand, Order>()
            .ForMember(dest => dest.Products, opt => opt.Ignore());
        CreateMap<DeleteOrderCommand, Order>();
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        CreateMap<OrderProduct, ProductOrderDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}
