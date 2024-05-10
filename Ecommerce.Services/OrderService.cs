using Catalog.Domain;
using Catalog.Facade.Services;
using Ecommerce.DTO;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service;

public class OrderService(CatalogDbContext context) : IOrderService
{
    private readonly CatalogDbContext _context = context;

    public async Task AddAsync(Order order)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(order); 
            Product product;
            foreach (var orderDetails in order.OrderDetails)
            {
                product = await _context.Products.SingleOrDefaultAsync(p => p.Id == orderDetails.ProductId);
                order.TotalAmount += product.UnitPrice * orderDetails.Quantity;
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
        
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