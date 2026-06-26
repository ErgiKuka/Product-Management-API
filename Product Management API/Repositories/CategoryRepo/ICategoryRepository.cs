using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.CategoryRepo
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}