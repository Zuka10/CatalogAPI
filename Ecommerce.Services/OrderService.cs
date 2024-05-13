using Catalog.Domain;
using Catalog.Facade.Services;
using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service;

public class OrderService(CatalogDbContext context, IProductService productService) : IOrderService
{
    private readonly CatalogDbContext _context = context;
    private readonly IProductService _productService = productService;

    public async Task AddAsync(Order order)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(order); 
            Product product;
            foreach (var orderDetails in order.OrderDetails)
            {
                product = await _productService.GetByIdAsync(orderDetails.ProductId);
                if (product is null)
                    throw new KeyNotFoundException($"Product with ID {orderDetails.ProductId} not found.");

                order.TotalAmount += product.UnitPrice * orderDetails.Quantity;

                await _context.OrderDetails.AddAsync(orderDetails);
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        catch(KeyNotFoundException) 
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task UpdateAsync(int id, Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.OrderDetails)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.OrderDetails)
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync();
    }
}