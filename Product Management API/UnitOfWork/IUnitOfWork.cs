using Product_Management_API.Repositories.CategoryRepo;
using Product_Management_API.Repositories.ProductRepo;

namespace Product_Management_API.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }

        Task<int> CompleteAsync();
    }
}
