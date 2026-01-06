using AutoMapper;
using OnionArch.Application.Dtos.Category;
using OnionArch.Domain.Entities;

namespace OnionArch.Application.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryReturnDto>()
            .ConstructUsing(c => new CategoryReturnDto(c.Id, c.Name));

        CreateMap<CategoryCreateDto, Category>();
    }
}
