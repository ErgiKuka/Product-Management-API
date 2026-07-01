using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.Orders;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.OrderService
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrdersService> _logger;
        private readonly IMapper _mapper;

        public OrdersService(IUnitOfWork unitOfWork
                            ,ILogger<OrdersService> logger
                            ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<OrdersResponseDto> CreateOrderAsync(OrdersCreateDto dto)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(dto.CustomerId);
            if (order != null)
            {
                throw new KeyNotFoundException($"Order with ID {dto.CustomerId} already exists.");
            }

            var orderItem = new Orders
            {
                CustomerId = dto.CustomerId,
                ShippingAddress = dto.ShippingAddress,
                OrderDate = DateTime.UtcNow,
                TotalAmount = dto.OrderItems.Sum(item => item.Quantity * item.UnitPrice),
                OrderStatus = "Pending"
            };

            await _unitOfWork.Orders.AddOrderAsync(orderItem);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Order with ID {orderItem.OrderId} created successfully.", orderItem.OrderId);
            return _mapper.Map<OrdersResponseDto>(orderItem);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} does not exist.");
            }

            _unitOfWork.Orders.DeleteOrder(order);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Order with ID {id} deleted successfully.", id);

        }

        public async Task<IEnumerable<OrdersResponseDto>> GetAllOrdersAsync(int? customerId)
        {
            var orders = await _unitOfWork.Orders.GetAllOrdersAsync(customerId);
            if (orders == null)
            {
                throw new KeyNotFoundException("No orders found.");
            }

            _logger.LogInformation("Retrieved {count} orders successfully for customer {customerId}", orders.Count(), customerId);

            return _mapper.Map<IEnumerable<OrdersResponseDto>>(orders).ToList();

        }

        public async Task<OrdersResponseDto> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} does not exist.");
            }

            _logger.LogInformation("Retrieved order with ID {id} successfully.", id);
            return _mapper.Map<OrdersResponseDto>(order);
        }

        public async Task UpdateOrderAsync(int id, OrdersUpdateDto dto)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} does not exist.");
            }

            order.CustomerId = dto.CustomerId;
            order.ShippingAddress = dto.ShippingAddress;

            _unitOfWork.Orders.UpdateOrder(order);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("Order with ID {id} updated successfully.", id);
        }
    }
}
