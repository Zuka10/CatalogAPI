using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;

namespace Ecommerce.Service;

public class CategoryService(IUnitOfWork unitOfWork, EcommerceDbContext context) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly EcommerceDbContext _context = context;

    public void Add(Category category)
    {
        _unitOfWork.CategoryRepository.Insert(category);
        _unitOfWork.SaveChanges();
    }

    public async Task AddAsync(Category category)
    {
        await _unitOfWork.CategoryRepository.InsertAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public void Update(int id, Category category)
    {
        var existingCategory = _unitOfWork.CategoryRepository.Get(id);
        if (existingCategory is not null)
        {
            existingCategory.Name = category.Name;

            _unitOfWork.CategoryRepository.Update(existingCategory);
            _unitOfWork.SaveChanges();
        }
    }

    public async Task<bool> UpdateAsync(int id, Category category)
    {
        var existingCategory = await _unitOfWork.CategoryRepository.GetAsync(id);
        if (existingCategory is not null)
        {
            existingCategory.Name = category.Name;
            await _unitOfWork.CategoryRepository.UpdateAsync(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public void Delete(int id)
    {
        var category = _unitOfWork.CategoryRepository.Get(id);
        if (category is not null)
        {
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _unitOfWork.CategoryRepository.GetAsync(id);
        if (category is not null)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public IEnumerable<Category> GetAll()
    {
        return _unitOfWork.CategoryRepository.GetAll();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _unitOfWork.CategoryRepository.GetAllAsync();
    }

    public Category GetById(int id)
    {
        return _unitOfWork.CategoryRepository.Get(id);
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _unitOfWork.CategoryRepository.GetAsync(id);
    }
}