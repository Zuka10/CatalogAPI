using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICountryService
{
    void Add(Country country);
    void Update(Country country);
    void Delete(int id);
    Country GetById(int id);
    IEnumerable<Country> GetAll();
    Task<Country> GetByIdAsync(int id);
}