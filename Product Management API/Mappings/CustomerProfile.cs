using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Customer;

namespace Product_Management_API.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerResponseDto>();
        }
    }
}
