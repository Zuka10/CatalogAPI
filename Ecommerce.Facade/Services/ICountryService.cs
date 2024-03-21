using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICountryService
{
    void Add(Country country);
    void Update(Country country);
    void Delete(int id);
    Country GetById(int id);
    IEnumerable<Country> GetAll();
    //Async methods
    Task<Country> GetByIdAsync(int id);
    Task CreateAsync(Country country);
    Task<bool> UpdateAsync(int id, Country country);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Country>> GetAllAsync();
}