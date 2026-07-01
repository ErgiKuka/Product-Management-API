namespace Product_Management_API.DTO.OrderItems
{
    public class OrderItemsUpdateDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
