using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.CustomerRepo;
using Product_Management_API.Repositories.OrderItemsRepo;
using Product_Management_API.Repositories.OrdersRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        ICustomerRepository Customer { get; }
        IOrdersRepository Orders { get; }
        IOrderItemsRepository OrderItems { get; }

        Task<int> CompleteAsync();

    }
}
