using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service;

public class CategoryService(CatalogDbContext context) : ICategoryService
{
    private readonly CatalogDbContext _context = context;

    public async Task AddAsync(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory is not null)
        {
            existingCategory.Name = category.Name;
            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
    }
}