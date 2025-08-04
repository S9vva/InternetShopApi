using InternetShopApi.Data.Data;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace InternetShopApi.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InternetShopDbContext _context;

        public CustomerRepository(InternetShopDbContext context) => _context = context;

        public async Task<Customer?> GetByIdAsync(int id) =>
            await _context.Customers.FindAsync(id);

        public async Task<IEnumerable<Customer>> GetAllAsync() =>
            await _context.Customers
            .Include(c => c.CustomerId)
            .Include(c => c.Name)
            .Include(c => c.SurName)
            .Include(c => c.Email)
                .ToListAsync();

        public async Task<Customer?> CreateAsync(Customer customer)
        {
            var customers = await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null) return false;

            _context.Remove(customers);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
