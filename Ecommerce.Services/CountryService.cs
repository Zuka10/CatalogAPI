using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;

namespace Ecommerce.Services;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    private readonly ICountryRepository _countryRepository = countryRepository;

    public void Add(Country country)
    {
        _countryRepository.Insert(country);
    }

    public void Update(Country country)
    {
        _countryRepository.Update(country);
    }

    public void Delete(int id)
    {
        var country = _countryRepository.Get(id);
        if (country is not null)
        {
            _countryRepository.Delete(country);
        }
    }

    public IEnumerable<Country> GetAll()
    {
        return _countryRepository.GetAll();
    }

    public Country GetById(int id)
    {
        return _countryRepository.Get(id);
    }

    public async Task<Country> GetByIdAsync(int id)
    {
        return await _countryRepository.GetAsync(id);
    }
}