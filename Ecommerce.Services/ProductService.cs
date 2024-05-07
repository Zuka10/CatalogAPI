using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service;

public class ProductService(CatalogDbContext context) : IProductService
{
    private readonly CatalogDbContext _context = context;

    public async Task AddAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        try
        {
            var existingProduct = await _context.Products
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (existingProduct is not null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.CategoryId = product.CategoryId;

                foreach (var existingImage in existingProduct.Images!.ToList())
                {
                    // Delete from database
                    _context.Images.Remove(existingImage);
                    // Delete from wwwroot/images directory
                    var filePath = Path.Combine("wwwroot/", existingImage.ImageUrl);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    existingProduct.Images?.Remove(existingImage);
                }

                // Add new images
                if (product.Images != null && product.Images.Count > 0)
                {
                    foreach (var image in product.Images)
                    {
                        existingProduct.Images?.Add(image);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Images)
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .SingleOrDefaultAsync();
    }
}