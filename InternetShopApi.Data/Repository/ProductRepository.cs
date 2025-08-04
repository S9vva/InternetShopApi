using InternetShopApi.Data.Data;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopApi.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InternetShopDbContext _context;

        public ProductRepository(InternetShopDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products
            .Include(p => p.Category)
            .ToListAsync();


        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null) return false;

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
