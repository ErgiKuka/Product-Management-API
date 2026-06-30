using AutoMapper;
using Product_Management_API.Data;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.ProductServ
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,
                             ILogger<ProductService> logger, 
                             IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
        {
            var category =  await _unitOfWork.Category.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.CategoryId} does not exist.");
            }

            var product = new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                Category = category,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };
            

            await _unitOfWork.Product.AddAsync(product);

            await _unitOfWork.CompleteAsync();

            _logger.LogInformation($"Product with ID {product.ProductId} created successfully.");
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} does not exist.");
            }

            _unitOfWork.Product.Delete(product);

            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync(int? categoryid)
        {
            var products = await _unitOfWork.Product.GetAllProductsAsync(categoryid);
            _logger.LogInformation("Retrieved {ProductCount} products from the database for categoryId {Categoryid}."
                                    ,products.Count()
                                    ,categoryid);

            return _mapper.Map<IEnumerable<ProductResponseDto>>(products).ToList();
        }

        public async Task<ProductResponseDto> GetProductsByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} does not exist.");
            }
            _logger.LogInformation("Retrieved product with ID {id} from the database.", id);

            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task UpdateProductAsync(ProductUpdateDto dto, int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} does not exist.");
            }

            var category = await _unitOfWork.Category.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {dto.CategoryId} does not exist.");
            }

            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Product with ID {id} updated successfully.", id);
        }
    }
}
