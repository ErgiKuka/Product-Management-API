using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;

namespace Product_Management_API.Mappings
{
    public class CategoryProfile : Profile  
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
