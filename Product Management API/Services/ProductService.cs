using Product_Management_API.Data;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Product;
using Product_Management_API.UnitOfWork;

namespace Product_Management_API.Services
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
            var category =  _unitOfWork.Category.GetByIdAsync(dto.CategoryId).Result;
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

            await _unitOfWork.Product.AddAsync(product);

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
            var product = _unitOfWork.Product.GetByIdAsync(id).Result;
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            _unitOfWork.Product.Delete(product);

            await _unitOfWork.CompleteAsync();

            return;

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = _unitOfWork.Product.GetAllProductsAsync();


            return await products;
        }

        public async Task<Product> GetProductsByIdAsync(int id)
        {
            var product = _unitOfWork.Product.GetByIdAsync(id).Result;
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            return product;

        }

        public async Task UpdateProductAsync(ProductUpdateDto dto, int id)
        {
            var product = _unitOfWork.Product.GetByIdAsync(id).Result;

            if (product == null)
            {
                throw new ArgumentException($"Product with ID {id} does not exist.");
            }

            var category = _unitOfWork.Category.GetByIdAsync(dto.CategoryId).Result;
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");
            }

            var updatedProduct = new Product
            {
                ProductId = product.ProductId,
                ProductName = dto.ProductName,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId
            };

            _unitOfWork.Product.Update(updatedProduct);
            await _unitOfWork.CompleteAsync();
        }
    }
}
