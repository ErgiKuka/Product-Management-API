using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.ProductRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(int? categoryid);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}