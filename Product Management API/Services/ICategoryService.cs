using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;

namespace Product_Management_API.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryById();
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync (int id, CategoryUpdateDto dto);
        Task DeleteCategoryAsync (int id);
    }
}
