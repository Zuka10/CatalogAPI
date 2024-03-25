using Ecommerce.DTO;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Facade.Services;

public interface IProductService
{
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
    Product GetById(int id);
    IEnumerable<Product> GetAll();
    //Async methods
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task<bool> UpdateAsync(int id, Product product);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
}