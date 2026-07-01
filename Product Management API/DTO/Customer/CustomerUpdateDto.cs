using Product_Management_API.DTO.Orders;

namespace Product_Management_API.DTO.Customer
{
    public class CustomerUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
