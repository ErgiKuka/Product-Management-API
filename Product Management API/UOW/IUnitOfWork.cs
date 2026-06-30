using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        Task<int> CompleteAsync();

    }
}
