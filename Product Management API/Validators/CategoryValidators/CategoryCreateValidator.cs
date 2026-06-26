using FluentValidation;
using Product_Management_API.Data;
using Product_Management_API.DTO.Category;
using Product_Management_API.UOW;

namespace Product_Management_API.Validators.CategoryValidators
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
        }
    }
}
