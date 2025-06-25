using AutoMapper;
using ManaFood.Application.Dtos;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.CreateCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.DeleteCategory;
using ManaFood.Application.UseCases.CategoryUseCase.Commands.UpdateCategory;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.Mappings;

public sealed class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();
        CreateMap<DeleteCategoryCommand, Category>();
        CreateMap<Category, CategoryDto>();
    }
}
