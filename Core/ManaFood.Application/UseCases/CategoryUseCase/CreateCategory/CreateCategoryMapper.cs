using AutoMapper;
using ManaFood.Domain.Entities;

namespace ManaFood.Application.UseCases.CategoryUseCase.CreateCategory;

public sealed class CreateCategoryMapper : Profile
{
    public CreateCategoryMapper()
    {
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CreateCategoryResponse>();
    }
}
