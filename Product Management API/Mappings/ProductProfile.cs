using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;

namespace Product_Management_API.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt
                .MapFrom(src => src.Category.CategoryName));
        }
    }
}
