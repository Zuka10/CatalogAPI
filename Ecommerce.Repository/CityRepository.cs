using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;

namespace Ecommerce.Repository;

public class CityRepository(EcommerceDbContext context) : RepositoryBase<City>(context), ICityRepository { }