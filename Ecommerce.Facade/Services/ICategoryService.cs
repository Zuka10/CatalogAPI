using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICategoryService
{
    Task<Category> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task<bool> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
}