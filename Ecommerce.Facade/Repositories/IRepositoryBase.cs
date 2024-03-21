using System.Linq.Expressions;

namespace Ecommerce.Facade;

public interface IRepositoryBase<TEntity>
{
    TEntity Get(params object[] id);
    IQueryable<TEntity> Set(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetAll();
    IQueryable<TEntity> Set();
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entity);
    //Async methods
    Task<TEntity> GetAsync(params object[] id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(object id);
    Task DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> SetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> SetAsync();
}