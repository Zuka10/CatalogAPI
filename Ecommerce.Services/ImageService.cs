using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;

namespace Ecommerce.Service;

public class ImageService(IUnitOfWork unitOfWork) : IImageService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(Image image)
    {
        await _unitOfWork.ImageRepository.InsertAsync(image);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, Image image)
    {
        try
        {
            var existingImage = await _unitOfWork.ImageRepository.GetAsync(id);
            var existingProduct = await _unitOfWork.ProductRepository.GetAsync(image.ProductId);
            if (existingImage is not null && existingProduct is not null)
            {
                existingImage.ProductId = image.ProductId;
                existingImage.ImageUrl = image.ImageUrl;

                await _unitOfWork.ImageRepository.UpdateAsync(existingImage);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await _unitOfWork.ImageRepository.GetAsync(id);
        if (image is not null)
        {
            await _unitOfWork.ImageRepository.DeleteAsync(image);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _unitOfWork.ImageRepository.GetAllAsync();
    }

    public async Task<Image> GetByIdAsync(int id)
    {
        return await _unitOfWork.ImageRepository.GetAsync(id);
    }
}