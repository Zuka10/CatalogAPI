using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;

namespace Ecommerce.Service;

public class CityService(ICityRepository cityRepository) : ICityService
{
    private readonly ICityRepository _cityRepository = cityRepository;

    public void Add(City city)
    {
        _cityRepository.Insert(city);
    }

    public void Update(City city)
    {
        _cityRepository.Update(city);
    }

    public void Delete(int id)
    {
        var city = _cityRepository.Get(id);
        if (city is not null)
        {
            _cityRepository.Delete(city);
        }
    }

    public IEnumerable<City> GetAll()
    {
        return _cityRepository.GetAll();
    }

    public City GetById(int id)
    {
        return _cityRepository.Get(id);
    }
}