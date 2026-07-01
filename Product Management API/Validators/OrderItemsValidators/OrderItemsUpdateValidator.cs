using FluentValidation;
using Product_Management_API.DTO.OrderItems;

namespace Product_Management_API.Validators.OrderItemsValidators
{
    public class OrderItemsUpdateValidator : AbstractValidator<OrderItemsUpdateDto>
    {
        public OrderItemsUpdateValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId is required.")
                .GreaterThan(0).WithMessage("OrderId must be greater than 0.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
