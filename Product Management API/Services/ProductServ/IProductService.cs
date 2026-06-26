using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;

namespace Product_Management_API.Services.ProductServ
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto> GetProductsByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(ProductUpdateDto dto, int id);
        Task DeleteProductAsync(int id);
    }
}
