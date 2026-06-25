using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;

namespace Product_Management_API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductsByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(ProductUpdateDto dto, int id);
        Task DeleteProductAsync(int id);
    }
}
