using InternetShopApi.Data.Data;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopApi.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InternetShopDbContext _context;

        public OrderRepository(InternetShopDbContext context) => _context = context;

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) =>
            await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync();
        public async Task<OrderItem?> GetByIdItemAsync(int id) =>
            await _context.Items.FindAsync(id);
        public async Task<Order> CreateAsync(Order order)
        {
            var orders = await _context.Orders.AddAsync(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null) return false;

            _context.Orders.Remove(orders);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(OrderItem order)
        {
            _context.Items.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
