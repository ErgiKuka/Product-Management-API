using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChangesAsync();
    }
}
