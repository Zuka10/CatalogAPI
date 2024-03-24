using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;

namespace Ecommerce.Repository;

public class ProductRepository(EcommerceDbContext context) : RepositoryBase<Product>(context), IProductRepository { }