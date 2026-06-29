using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Category;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.CategoryServ
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, 
                               ILogger<CategoryService> logger, 
                               IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
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

            _logger.LogInformation($"Category created with ID: {category.CategoryId}");

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} does not exist.");
            }
            _unitOfWork.Category.Delete(category);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation($"Category with ID: {id} deleted.");
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Category.GetAllCategoriesAsync();
            _logger.LogInformation($"Retrieved {categories.Count()} categories.");

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories).ToList();
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} does not exist.");
            }
            _logger.LogInformation($"Category retrieved with ID: {id}");

            return _mapper.Map<CategoryResponseDto>(category);
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
            _logger.LogInformation($"Category with ID: {id} updated.");
        }
    }
}
