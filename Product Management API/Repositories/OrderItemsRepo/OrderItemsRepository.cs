using Product_Management_API.Data;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.OrderItemsRepo
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly AppDbContext _context;

        public OrderItemsRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderAsync(OrderItems oi)
        {
            await _context.OrderItems.AddAsync(oi);
        }

        public void DeleteOrder(OrderItems oi)
        {
            _context.OrderItems.Remove(oi);
        }

        public async Task<IEnumerable<OrderItems>> GetAllOrdersAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItems> GetOrderByIdAsync(int orderItemsId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderItemId == orderItemsId);
        }

        public void UpdateOrder(OrderItems oi)
        {
            _context.OrderItems.Update(oi);
        }
    }
}
