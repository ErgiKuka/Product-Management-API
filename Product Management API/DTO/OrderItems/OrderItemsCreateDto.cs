namespace Product_Management_API.DTO.OrderItems
{
    public class OrderItemsCreateDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
