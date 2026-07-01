using AutoMapper;

namespace Product_Management_API.Mappings
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile() 
        {
            CreateMap<DTO.Orders.OrdersCreateDto, Data.Entities.Orders>();
            CreateMap<DTO.Orders.OrdersUpdateDto, Data.Entities.Orders>();
            CreateMap<Data.Entities.Orders, DTO.Orders.OrdersResponseDto>();
        }
    }
}
