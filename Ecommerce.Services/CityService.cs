using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;

namespace Ecommerce.Service;

public class CityService(IUnitOfWork unitOfWork) : ICityService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public void Add(City city)
    {
        _unitOfWork.CityRepository.Insert(city);
        _unitOfWork.SaveChanges();
    }

    public void Update(City city)
    {
        _unitOfWork.CityRepository.Update(city);
        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var city = _unitOfWork.CityRepository.Get(id);
        if (city is not null)
        {
            _unitOfWork.CityRepository.Delete(city);
            _unitOfWork.SaveChanges();
        }
    }

    public IEnumerable<City> GetAll()
    {
        return _unitOfWork.CityRepository.GetAll();
    }

    public City GetById(int id)
    {
        return _unitOfWork.CityRepository.Get(id);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _unitOfWork.CityRepository.GetAllAsync();
    }

    public async Task<City> GetByIdAsync(int id)
    {
        return await _unitOfWork.CityRepository.GetAsync(id);
    }

    public async Task AddAsync(City city)
    {
        var existingCountry = await _unitOfWork.CountryRepository.GetAsync(city.CountryId);
        if (existingCountry is not null)
        {
            city.CountryId = existingCountry.Id;
            city.Country = existingCountry;
            await _unitOfWork.CityRepository.InsertAsync(city);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<bool> UpdateAsync(int id, City city)
    {
        var existingCity = await _unitOfWork.CityRepository.GetAsync(id);
        var existingCountry = await _unitOfWork.CountryRepository.GetAsync(city.CountryId);
        if (existingCity is null || existingCountry is null)
        {
            return false;
        }

        existingCity.Name = city.Name;
        existingCity.CountryId = city.CountryId;

        await _unitOfWork.CityRepository.UpdateAsync(existingCity);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var city = await _unitOfWork.CityRepository.GetAsync(id);
        if (city is not null)
        {
            await _unitOfWork.CityRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }
}