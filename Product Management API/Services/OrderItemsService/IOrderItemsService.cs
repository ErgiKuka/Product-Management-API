using Product_Management_API.DTO.OrderItems;

namespace Product_Management_API.Services.OrderItemsService
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItemsResponseDto>> GetOrderItemsAsync();
        Task<OrderItemsResponseDto> GetOrderItemByIdAsync(int id);
        Task<OrderItemsResponseDto> AddItemToOrderAsync(OrderItemsCreateDto dto);
        Task UpdateOrderItemAsync(int id, OrderItemsUpdateDto dto);
        Task DeleteOrderItemAsync(int id);
    }
}
