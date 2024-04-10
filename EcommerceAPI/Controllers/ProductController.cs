using Ecommerce.API.Models;
using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("[controller]")]
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

            var response = products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                unitPrice = p.UnitPrice,
                categoryId = p.CategoryId,
                category = p.Category?.Name,
                images = p.Images!.Select(image => $"{image}")
            });

            return Ok(response);
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
                return NotFound();

            var response = new
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                unitPrice = product.UnitPrice,
                categoryId = product.CategoryId,
                category = product.Category?.Name,
                images = product.Images?.Select(image => $"{image}")
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
    public async Task<IActionResult> Create([FromForm] ProductModel productModel)
    {
        try
        {
            var imageUrls = new List<string>();

            if (productModel.Images != null && productModel.Images.Count > 0)
            {
                foreach (var formFile in productModel.Images)
                {
                    if (formFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                        var filePath = Path.Combine("wwwroot/images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }

                        imageUrls.Add("images/" + fileName);
                    }
                }
            }

            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                UnitPrice = productModel.UnitPrice,
                CategoryId = productModel.CategoryId,
                Images = imageUrls
            };

            await _productService.AddAsync(product);

            return Ok("Created Successfully");
        }
        catch (ArgumentNullException)
        {
            return BadRequest("required property is null");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {productModel.CategoryId} not found");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductModel productModel)
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
    public async Task<IActionResult> Delete(int id)
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