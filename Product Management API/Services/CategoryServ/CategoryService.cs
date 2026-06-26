using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;
using Product_Management_API.UnitOfWork;

namespace Product_Management_API.Services.CategoryServ
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };
            await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} does not exist.");
            }
            _unitOfWork.Category.Delete(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            return categories.Select(c => new CategoryResponseDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                Description = c.Description
            }).toList();
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} does not exist.");
            }
            return new CategoryResponseDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} does not exist.");
            }
            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;
            _unitOfWork.Category.Update(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}
