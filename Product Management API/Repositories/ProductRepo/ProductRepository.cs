using Product_Management_API.Data;
using Product_Management_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Product_Management_API.Repositories.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int? categoryid)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryid.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryid.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId)
                                          .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category)
                                           .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}