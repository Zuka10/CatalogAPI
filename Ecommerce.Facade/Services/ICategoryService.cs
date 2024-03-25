using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICategoryService
{
    void Add(Category category);
    void Update(int id, Category category);
    void Delete(int id);
    Category GetById(int id);
    IEnumerable<Category> GetAll();
    //Async methods
    Task<Category> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task<bool> UpdateAsync(int id, Category category);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
}