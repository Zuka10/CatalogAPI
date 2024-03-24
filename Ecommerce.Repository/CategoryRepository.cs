using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;

namespace Ecommerce.Repository;

public class CategoryRepository(EcommerceDbContext context) : RepositoryBase<Category>(context), ICategoryRepository { }