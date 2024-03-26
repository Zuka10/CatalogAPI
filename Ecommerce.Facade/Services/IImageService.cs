using Ecommerce.DTO;

namespace Ecommerce.Facade.Services;

public interface IImageService
{
    Task<Image> GetByIdAsync(int id);
    Task AddAsync(Image image);
    Task<bool> UpdateAsync(int id, Image image);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Image>> GetAllAsync();
}