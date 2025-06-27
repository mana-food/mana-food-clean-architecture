using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.ProductUseCase.Commands.CreateProduct;
using ManaFood.Application.UseCases.ProductUseCase.Commands.DeleteProduct;
using ManaFood.Application.UseCases.ProductUseCase.Commands.UpdateProduct;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Mappings;

public sealed class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Items, opt => opt.Ignore()); 
        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Items, opt => opt.Ignore());
        CreateMap<DeleteProductCommand, Product>();
        CreateMap<Product, ProductDto>();
    }
}
