using Ecommerce.API.Models;
using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ImageController(IImageService imageService) : ControllerBase
{
    private readonly IImageService _imageService = imageService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var images = await _imageService.GetAllAsync();
            return Ok(images);
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
            var image = await _imageService.GetByIdAsync(id);
            if (image is null)
                return NotFound();

            var response = new
            {
                id = image.Id,
                imageUrl = image.ImageUrl,
                productId = image.ProductId
            };

            return Ok(image);
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
    public async Task<IActionResult> Create(ImageModel imageModel)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            foreach (var item in imageModel.Images)
            {
                await item.CopyToAsync(memoryStream);
            }

            var image = new Image
            {
                ImageUrl = memoryStream.ToArray(),
                ProductId = imageModel.ProductId
            };

            await _imageService.AddAsync(image);
            return Ok("Created Successfully");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {imageModel.ProductId} not found");
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
    public async Task<IActionResult> Update(int id, ImageModel imageModel)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            foreach (var item in imageModel.Images)
            {
                await item.CopyToAsync(memoryStream);
            }

            var image = new Image
            {
                ImageUrl = memoryStream.ToArray(),
                ProductId = imageModel.ProductId
            };

            await _imageService.UpdateAsync(id, image);
            return Ok("Updated Successfully");
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} or {imageModel.ProductId} not found");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _imageService.DeleteAsync(id);
            return Ok("Deleted Successfuly");
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