using AutoMapper;
using Product_Management_API.Data.Entities;
using Product_Management_API.DTO.OrderItems;
using Product_Management_API.UOW;

namespace Product_Management_API.Services.OrderItemsService
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderItemsService> _logger;
        private readonly IMapper _mapper;

        public OrderItemsService(IUnitOfWork unitOfWork
                            , ILogger<OrderItemsService> logger
                            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<OrderItemsResponseDto> AddItemToOrderAsync(OrderItemsCreateDto dto)
        {
           Orders order =await _unitOfWork.Orders.GetOrderByIdAsync(dto.OrderId);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {dto.OrderId} not found.");
            }

           Product? product = await _unitOfWork.Product.GetByIdAsync(dto.ProductId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {dto.ProductId} not found.");
            }

            var orderItem = new OrderItemsCreateDto
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = product.Price
            };

            decimal totalAmmount = orderItem.Quantity * orderItem.UnitPrice;

            order.TotalAmount += totalAmmount;

            await _unitOfWork.OrderItems.AddOrderAsync(_mapper.Map<OrderItems>(orderItem));
            _logger.LogInformation("OrderItem added to Order with ID {dto.OrderId}.", dto.OrderId);
            _unitOfWork.Orders.UpdateOrder(order);
            _logger.LogInformation("Order with ID {dto.OrderId} updated with new total amount: {order.TotalAmount}.", dto.OrderId ,order.TotalAmount);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderItemsResponseDto>(orderItem);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderid = _unitOfWork.OrderItems.GetOrderByIdAsync(id).Result;
            if (orderid == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }
             _unitOfWork.OrderItems.DeleteOrder(orderid);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("OrderItem with ID {id} has been deleted.", id);
        }

        public async Task<OrderItemsResponseDto> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _unitOfWork.OrderItems.GetOrderByIdAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }
            _logger.LogInformation("OrderItem with ID {id} retrieved successfully.", id);
            return await _mapper.Map<Task<OrderItemsResponseDto>>(orderItem);
        }

        public async Task<IEnumerable<OrderItemsResponseDto>> GetOrderItemsAsync()
        {
            var orderItems = await _unitOfWork.OrderItems.GetAllOrdersAsync();
            if (orderItems == null || !orderItems.Any())
            {
                throw new KeyNotFoundException("No OrderItems found.");
            }
            _logger.LogInformation("OrderItems retrieved successfully.");
            return await _mapper.Map<Task<IEnumerable<OrderItemsResponseDto>>>(orderItems);
        }

        public async Task UpdateOrderItemAsync(int id, OrderItemsUpdateDto dto)
        {
            var orderItem = _unitOfWork.OrderItems.GetOrderByIdAsync(id).Result;
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }

            orderItem.Quantity = dto.Quantity;
            orderItem.ProductId = dto.ProductId;
            orderItem.OrderId = dto.OrderId;
            
            _unitOfWork.OrderItems.UpdateOrder(orderItem);
            await _unitOfWork.CompleteAsync();
            _logger.LogInformation("OrderItem with ID {id} updated successfully.", id);
        }
    }
}
