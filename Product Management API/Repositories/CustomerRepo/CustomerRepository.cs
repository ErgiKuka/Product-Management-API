using Product_Management_API.Data;
using Microsoft.EntityFrameworkCore;
using Product_Management_API.Data.Entities;

namespace Product_Management_API.Repositories.CustomerRepo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
           return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
        }
    }
}
