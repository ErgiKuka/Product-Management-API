namespace Product_Management_API.DTO.Orders
{
    public class OrdersResponseDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}
