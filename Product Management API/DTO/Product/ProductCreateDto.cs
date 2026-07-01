namespace Product_Management_API.DTO.Product
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } = 0;
        public int CategoryId { get; set; }
    }
}
