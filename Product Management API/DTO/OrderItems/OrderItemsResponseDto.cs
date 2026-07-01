namespace Product_Management_API.DTO.OrderItems
{
    public class OrderItemsResponseDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal UnitPrice { get; set; }
    }
}
