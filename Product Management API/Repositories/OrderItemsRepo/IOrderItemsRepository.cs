using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.OrderItemsRepo
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItems>> GetAllOrdersAsync();
        Task<OrderItems> GetOrderByIdAsync(int orderItemsId);
        Task AddOrderAsync(OrderItems oi);
        void UpdateOrder(OrderItems oi);
        void DeleteOrder(OrderItems oi);
    }
}
