using Ecommerce.Facade.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Repository;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly EcommerceDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public UnitOfWork(EcommerceDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _countryRepository = new CountryRepository(context);
        _cityRepository = new CityRepository(context);
        _categoryRepository = new CategoryRepository(context);
        _productRepository = new ProductRepository(context);
    }

    public ICountryRepository CountryRepository => _countryRepository;
    public ICityRepository CityRepository => _cityRepository;
    public ICategoryRepository CategoryRepository => _categoryRepository;
    public IProductRepository ProductRepository => _productRepository;

    public void BeginTransaction()
    {
        try
        {
            _transaction = _context.Database.BeginTransaction();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to begin transaction");
            throw;
        }
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
            _transaction?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction");
            throw;
        }
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
    }

    public void SaveChanges()
    {
        try
        {
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error");
            throw;
        }

    }

    public void Dispose()
    {
        try
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction");
            throw;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DbContext error");
            throw;
        }
    }
}