using AutoMapper;
using Ecommerce.DTO;
using Ecommerce.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Catalog.gRPCService;

public class CategorygRPCService : CategoryService.CategoryServiceBase
{
    private readonly CatalogDbContext _context;
    private readonly IMapper _mapper;

    public CategorygRPCService(CatalogDbContext context)
    {
        _context = context;
        _mapper = MapperConfig.InitializeAutoMapper();
    }

    public override async Task<IdRequest> AddCategory(CategoryDTO request, ServerCallContext context)
    {
        try
        {
            var category = _mapper.Map<Category>(request);
            if (category == null)
            {
                throw new InvalidOperationException("Failed to map CategoryDTO to Category.");
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return new IdRequest { Id = category.Id };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public override async Task<Empty> UpdateCategory(CategoryDTO request, ServerCallContext context)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        if (category != null)
        {
            _mapper.Map(request, category);
            category.Name = request.Name;
            await _context.SaveChangesAsync();
        }
        return new Empty();
    }

    public override async Task<Empty> DeleteCategory(DeleteRequest request, ServerCallContext context)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return new Empty();
    }

    public override async Task<CategoryList> GetAllCategories(Empty request, ServerCallContext context)
    {
        var categories = await _context.Categories.ToListAsync();
        var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
        return new CategoryList { Categories = { categoryDTOs } };
    }

    public override async Task<CategoryDTO> GetCategoryById(IdRequest request, ServerCallContext context)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        return _mapper.Map<CategoryDTO>(category);
    }
}