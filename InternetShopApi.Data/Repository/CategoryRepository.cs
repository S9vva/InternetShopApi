using InternetShopApi.Data.Data;
using InternetShopApi.Data.Repository.Interfaces;
using InternetShopApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShopApi.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InternetShopDbContext _context;

        public CategoryRepository(InternetShopDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.FindAsync(id);

        public async Task<Category> CreateAsync(Category category)
        {
            var categories = await _context.Categories.AddAsync(category);
            _context.SaveChanges();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null) return false;
            
            var isUseInProduct = await _context.Products.AnyAsync(p => p.CategoryId == id);
            if (isUseInProduct)
            {
                throw new InvalidOperationException("It is impossible to delete a product because it is used in products.");
            }
            _context.Categories.Remove(categories);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
