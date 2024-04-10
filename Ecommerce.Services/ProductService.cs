using Ecommerce.DTO;
using Ecommerce.Facade.Repositories;
using Ecommerce.Facade.Services;
using Ecommerce.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Service;

public class ProductService(IUnitOfWork unitOfWork, EcommerceDbContext context) : IProductService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly EcommerceDbContext _context = context;

    public void Add(Product product)
    {
        _unitOfWork.ProductRepository.Insert(product);
        _unitOfWork.SaveChanges();
    }

    public async Task AddAsync(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        var category = await _unitOfWork.CategoryRepository.GetAsync(product.CategoryId);
        product.Category = category;
        await _unitOfWork.ProductRepository.InsertAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public void Update(Product product)
    {
        _unitOfWork.ProductRepository.Update(product);
    }

    public async Task<bool> UpdateAsync(int id, Product product)
    {
        var existingProduct = await _unitOfWork.ProductRepository.GetAsync(id);
        if (existingProduct is not null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Images = product.Images;

            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public void Delete(int id)
    {
        var product = _unitOfWork.ProductRepository.Get(id);
        if (product is not null)
        {
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(id);
        if (product is not null)
        {
            await _unitOfWork.ProductRepository.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public IEnumerable<Product> GetAll()
    {
        return _unitOfWork.ProductRepository.GetAll();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();
    }

    public Product GetById(int id)
    {
        return _unitOfWork.ProductRepository.Get(id);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .SingleAsync();
    }
}