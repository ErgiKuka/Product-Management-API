using Product_Management_API.DTO.OrderItems;

namespace Product_Management_API.DTO.Orders
{
    public class OrdersUpdateDto
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<OrderItemsCreateDto> OrderItems { get; set; } = new List<OrderItemsCreateDto>();
    }
}
