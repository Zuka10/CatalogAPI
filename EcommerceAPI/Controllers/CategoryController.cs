using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories is null || !categories.Any())
            {
                return NotFound("categories not found");
            }
            return Ok(categories);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        try
        {
            await _categoryService.AddAsync(category);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        try
        {
            await _categoryService.UpdateAsync(id, category);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteAsync(id);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}