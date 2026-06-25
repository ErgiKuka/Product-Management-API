using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data;
using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
        public void SaveChangesAsync()
        {
            _context.SaveChanges();
        }

    }
}
