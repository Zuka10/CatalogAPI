using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;

namespace Ecommerce.Services;

public class CountryService(IUnitOfWork unitOfWork) : ICountryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public void Add(Country country)
    {
        _unitOfWork.CountryRepository.Insert(country);
        _unitOfWork.SaveChanges();
    }

    public void Update(Country country)
    {
        _unitOfWork.CountryRepository.Update(country);
        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var country = _unitOfWork.CountryRepository.Get(id);
        if (country is not null)
        {
            _unitOfWork.CountryRepository.Delete(country);
            _unitOfWork.SaveChanges();
        }
    }

    public IEnumerable<Country> GetAll()
    {
        return _unitOfWork.CountryRepository.GetAll();
    }

    public Country GetById(int id)
    {
        return _unitOfWork.CountryRepository.Get(id);
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _unitOfWork.CountryRepository.GetAllAsync();
    }

    public async Task<Country> GetByIdAsync(int id)
    {
        return await _unitOfWork.CountryRepository.GetAsync(id);
    }

    public async Task AddAsync(Country country)
    {
        await _unitOfWork.CountryRepository.InsertAsync(country);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, Country country)
    {
        var existingCountry = await _unitOfWork.CountryRepository.GetAsync(id);
        if (existingCountry is null)
        {
            return false;
        }

        existingCountry.Name = country.Name;
        existingCountry.Cities = country.Cities;

        await _unitOfWork.CountryRepository.UpdateAsync(existingCountry);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var country = await _unitOfWork.CountryRepository.GetAsync(id);
        if (country is not null)
        {
            await _unitOfWork.CountryRepository.DeleteAsync(id);
            return true;
        }
        return false;
    }
}