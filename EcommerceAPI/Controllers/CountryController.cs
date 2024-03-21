using Ecommerce.DTO;
using Ecommerce.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var countries = await _countryService.GetAllAsync();
            if (countries is null || !countries.Any())
            {
                return NotFound("countries not found");
            }
            return Ok(countries);
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
            var country = await _countryService.GetByIdAsync(id);
            if (country is null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"record with key {id} not found");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Country country)
    {
        try
        {
            await _countryService.CreateAsync(country);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Country country)
    {
        try
        {
            var result = await _countryService.UpdateAsync(id, country);
            if (!result)
            {
                return NotFound(); // Country not found
            }

            return Ok(); // Update successful
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
            var country = await _countryService.DeleteAsync(id);
            if (!country)
            {
                return NotFound($"record with key {id} not found");
            }
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}