using Product_Management_API.DTO.Orders;

namespace Product_Management_API.Services.OrderService
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrdersResponseDto>> GetAllOrdersAsync(int? customerId);
        Task<OrdersResponseDto> GetOrderByIdAsync(int id);
        Task<OrdersResponseDto> CreateOrderAsync(OrdersCreateDto dto);
        Task UpdateOrderAsync(int id, OrdersUpdateDto dto);
        Task DeleteOrderAsync(int id);
    }
}
