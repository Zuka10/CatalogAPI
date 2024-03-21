using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICityService
{
    void Add(City city);
    void Update(City city);
    void Delete(int id);
    City GetById(int id);
    IEnumerable<City> GetAll();
    //Async methods
    Task<City> GetByIdAsync(int id);
    Task AddAsync(City city);
    Task<bool> UpdateAsync(int id, City city);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<City>> GetAllAsync();
}