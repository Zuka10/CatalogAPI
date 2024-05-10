using Catalog.Domain;

namespace Catalog.Facade.Services;

public interface IOrderService
{
    public Task<Order> GetByIdAsync(int id);
    public Task AddAsync(Order order);
}