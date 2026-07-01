using Product_Management_API.DTO.OrderItems;

namespace Product_Management_API.DTO.Orders
{
    public class OrdersCreateDto
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }

        public List<OrderItemsCreateDto> OrderItems { get; set; } = new List<OrderItemsCreateDto>();
    }
}
