using Catalog.Domain;

namespace Catalog.Facade.Services;

public interface IOrderService
{
    public Task<Order> GetByIdAsync(int id);
    public Task AddAsync(Order order);
    public Task UpdateAsync(int id, Order order);
    public Task<IEnumerable<Order>> GetAllAsync();
}