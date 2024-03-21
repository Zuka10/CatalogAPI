using Ecommerce.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository;

public class EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : DbContext(options)
{
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
}