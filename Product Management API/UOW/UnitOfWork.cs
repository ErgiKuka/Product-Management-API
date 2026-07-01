using Product_Management_API.Data;
using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.CustomerRepo;
using Product_Management_API.Repositories.OrderItemsRepo;
using Product_Management_API.Repositories.OrdersRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private bool _disposed = false;

        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IOrdersRepository Orders { get; private set; }
        public IOrderItemsRepository OrderItems { get; private set; }

        public UnitOfWork(AppDbContext context, 
                            IProductRepository productRepository, 
                            ICategoryRepository categoryRepository,
                            ICustomerRepository customerRepository,
                            IOrdersRepository ordersRepository,
                            IOrderItemsRepository orderItemsRepository)
        {
            _context = context;
            Product = productRepository;
            Category = categoryRepository;
            Customer = customerRepository;
            Orders = ordersRepository;
            OrderItems = orderItemsRepository;
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
