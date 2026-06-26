using Product_Management_API.Data;
using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(AppDbContext context, 
                            IProductRepository productRepository, 
                            ICategoryRepository categoryRepository)
        {
            _context = context;
            Product = productRepository;
            Category = categoryRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
