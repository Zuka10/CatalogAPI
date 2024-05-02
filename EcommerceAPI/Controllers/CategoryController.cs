using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            var response = categories.Select(c => new
            {
                id = c.Id,
                name = c.Name
            });

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category is null)
                return NotFound($"record with key {id} not found");

            var response = new
            {
                id = category.Id,
                name = category.Name
            };

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] string name)
    {
        try
        {
            var category = new Category { Name = name };
            await _categoryService.AddAsync(category);
            return Ok("Created Successfully");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] string name)
    {
        try
        {
            var category = new Category { Name = name };
            if (await _categoryService.UpdateAsync(id, category))
                return Ok("Updated Successfully");

            return NotFound($"record with key {id} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (await _categoryService.DeleteAsync(id))
                return Ok("Deleted Successfully");

            return NotFound($"record with key {id} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }
}