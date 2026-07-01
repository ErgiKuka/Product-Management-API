using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Customer;

namespace Product_Management_API.Services.CustomerServ
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
        Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
        Task<CustomerResponseDto> AddCustomerAsync(CustomerCreateDto dto);
        Task UpdateCustomerAsync(CustomerUpdateDto dto, int id);
        Task DeleteCustomerAsync(int id);
    }
}
