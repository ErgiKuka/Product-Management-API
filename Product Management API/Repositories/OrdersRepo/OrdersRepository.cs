using Product_Management_API.Data;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.OrdersRepo
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _context;

        public OrdersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Orders orders)
        {
            await _context.Orders.AddAsync(orders);
        }

        public async Task<IEnumerable<Orders>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }
        public void DeleteOrder(Orders orders)
        {
            _context.Orders.Remove(orders);
        }

        public async Task<IEnumerable<Orders>> GetAllOrdersAsync(int? customerId)
        {
            var query = _context.Customers.Include(p => p.CustomerId).AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(p => p.CustomerId == customerId.Value);
            }

            return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int ordersId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == ordersId);
        }

        public void UpdateOrder(Orders orders)
        {
            _context.Orders.Update(orders);
        }
    }
}
