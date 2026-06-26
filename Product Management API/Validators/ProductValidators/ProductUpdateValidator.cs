using FluentValidation;
using Product_Management_API.DTO.Product;

namespace Product_Management_API.Validators.ProductValidators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0).WithMessage("Stock cannot be negative");
        }
    }
}
