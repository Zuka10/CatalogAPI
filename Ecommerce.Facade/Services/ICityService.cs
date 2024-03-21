using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface ICityService
{
    void Add(City city);
    void Update(City city);
    void Delete(int id);
    City GetById(int id);
    IEnumerable<City> GetAll();
}