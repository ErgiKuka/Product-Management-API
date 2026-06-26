using Product_Management_API.Data;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.ProductServ
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponseDto> CreateProductAsync(ProductCreateDto dto)
        {
            var category =  await _unitOfWork.Category.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");
            }

            var product = new Product
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Product.AddAsync(product);

            await _unitOfWork.CompleteAsync();

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = category.CategoryName,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            _unitOfWork.Product.Delete(product);

            await _unitOfWork.CompleteAsync();

            return;

        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Product.GetAllProductsAsync();
            return products.Select(p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.CategoryName,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();
        }

        public async Task<ProductResponseDto> GetProductsByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }
            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryName = product.Category.CategoryName,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }

        public async Task UpdateProductAsync(ProductUpdateDto dto, int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            var category = await _unitOfWork.Category.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");
            }

            product.ProductName = dto.ProductName;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }
    }
}
