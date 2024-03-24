namespace Ecommerce.Facade.Repositories;

public interface IUnitOfWork
{
    ICountryRepository CountryRepository { get; }
    ICityRepository CityRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }

    void BeginTransaction();
    void Commit();
    void Rollback();
    void SaveChanges();
    Task SaveChangesAsync();
}