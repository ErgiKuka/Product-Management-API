using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.OrdersRepo
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> GetAllOrdersAsync(int? customerId);
        Task<Orders> GetOrderByIdAsync(int ordersId);
        Task<IEnumerable<Orders>> GetOrdersByCustomerIdAsync(int customerId);
        Task AddOrderAsync(Orders orders);
        void UpdateOrder(Orders orders);
        void DeleteOrder(Orders orders);
    }
}
