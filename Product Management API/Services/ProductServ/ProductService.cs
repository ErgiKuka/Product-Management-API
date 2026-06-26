using Product_Management_API.Data;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;
using Product_Management_API.UnitOfWork;

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
                CategoryId = dto.CategoryId
            };

            await _unitOfWork.Product.AddAsync(dto);

            await _unitOfWork.CompleteAsync();

            return new ProductResponseDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            _unitOfWork.Product.Delete(id);

            await _unitOfWork.CompleteAsync();

            return;

        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return products.Select(p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId
            }).toList();
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
                CategoryId = product.CategoryId
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
