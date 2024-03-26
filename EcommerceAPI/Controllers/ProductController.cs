using Ecommerce.API.Models;
using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController(IProductService productService, ICategoryService categoryService) : ControllerBase
{
    private readonly IProductService _productService = productService;
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            var response = products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                unitPrice = p.UnitPrice,
                category = p.Category?.Name
            });

            return Ok(response);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Show(int id)
    {
        try
        {
            var product = await _productService.GetByIdAsync(id);
            var category = await _categoryService.GetByIdAsync(product.CategoryId);
            if (product is null)
                return NotFound();

            var response = new
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                unitPrice = product.UnitPrice,
                category = category.Name
            };

            return Ok(response);
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
    public async Task<IActionResult> Store(ProductModel productModel)
    {
        try
        {
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId
            };

            await _productService.AddAsync(product);
            return Ok("Created Successfully");
        }
        catch (ArgumentNullException)
        {
            return BadRequest("required property is null");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductModel productModel)
    {
        try
        {
            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId
            };

            await _productService.UpdateAsync(id, product);
            return Ok("Updated successfully");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} not found");
        }
        catch (ArgumentNullException)
        {
            return BadRequest("required property is null");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Destroy(int id)
    {
        try
        {
            await _productService.DeleteAsync(id);
            return Ok("Deleted Successfully");
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