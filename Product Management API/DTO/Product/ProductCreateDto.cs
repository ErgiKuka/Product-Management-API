namespace Product_Management_API.DTO.Product
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
