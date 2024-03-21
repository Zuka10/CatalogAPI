namespace Ecommerce.Facade.Repositories;

public interface IUnitOfWork
{
    ICountryRepository CountryRepository { get; }
    ICityRepository CityRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}
