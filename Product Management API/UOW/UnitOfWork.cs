using Product_Management_API.Data;
using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private bool _disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
