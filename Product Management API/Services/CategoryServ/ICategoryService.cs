using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;

namespace Product_Management_API.Services.CategoryServ
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> GetCategoryById(int id);
        Task<CategoryResponseDto> CreateCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync (int id, CategoryUpdateDto dto);
        Task DeleteCategoryAsync (int id);
    }
}
