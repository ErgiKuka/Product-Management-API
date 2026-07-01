using FluentValidation;
using Product_Management_API.DTO.Orders;

namespace Product_Management_API.Validators.OrderValidator
{
    public class OrderUpdateValidator : AbstractValidator<OrdersUpdateDto>
    {
        public OrderUpdateValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0.");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("Shipping address is required.")
                .MaximumLength(200).WithMessage("Shipping address must not exceed 200 characters.");
        }
    }
}
