using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface IProductService
{
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task<bool> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
}