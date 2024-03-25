using Ecommerce.API.Models;
using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            if (products is null || !products.Any())
            {
                return NotFound("products not found");
            }
            return Ok(products);
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
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
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
    public async Task<IActionResult> Create(ProductModel productModel)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await productModel.Image.CopyToAsync(memoryStream);

            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Image = memoryStream.ToArray(),
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId
            };

            await _productService.AddAsync(product);
            return Ok();
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
            using var memoryStream = new MemoryStream();
            await productModel.Image.CopyToAsync(memoryStream);

            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Image = memoryStream.ToArray(),
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId
            };

            await _productService.UpdateAsync(id, product);
            return Ok();
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteAsync(id);
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