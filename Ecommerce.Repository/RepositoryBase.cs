using Ecommerce.Facade;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Repository;

public abstract class RepositoryBase<TEntity>(EcommerceDbContext context) : IRepositoryBase<TEntity>
    where TEntity : class
{
    protected readonly EcommerceDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public TEntity Get(params object[] id) => _dbSet.Find(id) ?? throw new KeyNotFoundException($"Record with key {id} not found");

    public IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

    public IQueryable<TEntity> Set() => _dbSet;

    public TEntity Find(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return _dbSet.AsNoTracking().SingleOrDefault(predicate)!;
    }

    public void Insert(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
    }

    public void Delete(object id)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));

        Delete(Get(id));
    }

    public void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public async Task<TEntity> GetAsync(params object[] id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"Record with key {id} not found");
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return await _dbSet.AsNoTracking().SingleOrDefaultAsync(predicate) ?? throw new KeyNotFoundException("Record not found");
    }

    public async Task InsertAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
        await Task.CompletedTask; // Entity Framework Core tracks changes automatically
    }

    public async Task DeleteAsync(object id)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));

        var entity = await GetAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
        await Task.CompletedTask; // Entity Framework Core tracks changes automatically
    }

    public async Task<IEnumerable<TEntity>> SetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> SetAsync()
    {
        return await _dbSet.ToListAsync();
    }
}