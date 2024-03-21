using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;

namespace Ecommerce.Repository;

public class CountryRepository(EcommerceDbContext context) : RepositoryBase<Country>(context), ICountryRepository { }