using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;

namespace Ecommerce.Repository;

public class ImageRepository(EcommerceDbContext context) : RepositoryBase<Image>(context), IImageRepository { }