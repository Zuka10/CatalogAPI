using Catalog.API.DTOs;
using Catalog.Domain;
using Catalog.Facade.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;

namespace Catalog.API.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize]
[EnableRateLimiting("fixed")]
public class OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager) : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IOrderService _orderService = orderService;

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var orders = await _orderService.GetAllAsync();
			return Ok(orders);
		}
		catch (Exception e)
		{
            Log.Error(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		try
		{
			var order = await _orderService.GetByIdAsync(id);
			if (order is null)
				return NotFound();

			var response = new
			{
				id = order.Id,
				orderDate = order.OrderDate,
				totalAmount = order.TotalAmount,
				userId = order.UserId,
				orderDetails = order.OrderDetails
			};

			return Ok(response);
		}
		catch (Exception e)
		{
            Log.Error(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
	}

    [HttpPost]
    public async Task<IActionResult> Create(OrderDTO orderModel)
    {
		try
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var order = new Order
            {
                User = user,
                UserId = user.Id,
				OrderDetails = orderModel.OrderDetails.Select(od => new OrderDetail
				{
					ProductId = od.ProductId,
					Quantity = od.Quantity
				}).ToList()
			};

			await _orderService.AddAsync(order);
			return Ok();
		}
		catch (KeyNotFoundException)
		{
			return BadRequest("product not found");
		}
		catch (Exception e)
		{
            Log.Error(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}