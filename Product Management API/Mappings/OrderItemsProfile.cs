using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;
namespace Product_Management_API.Mappings
{
    public class OrderItemsProfile : Profile
    {
        public OrderItemsProfile() 
        {
            CreateMap<DTO.OrderItems.OrderItemsCreateDto, Data.Entities.OrderItems>();
            CreateMap<DTO.OrderItems.OrderItemsUpdateDto, Data.Entities.OrderItems>();
            CreateMap<Data.Entities.OrderItems, DTO.OrderItems.OrderItemsResponseDto>();
        }
    }
}
