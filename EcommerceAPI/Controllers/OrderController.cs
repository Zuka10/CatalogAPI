using Catalog.API.Models;
using Catalog.Domain;
using Catalog.Facade.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager) : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IOrderService _orderService = orderService;

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
		catch (Exception)
		{
			throw;
		}
	}

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] OrderDTO orderModel)
    {
		try
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			var order = new Order
            {
                User = user,
                UserId = user.Id,
				OrderDetails = new List<OrderDetail>
				{
					new OrderDetail
					{
						ProductId = orderModel.ProductId,
						Quantity = orderModel.Quantity
					}
				}
			};

			await _orderService.AddAsync(order);
			return Ok();
		}
		catch (Exception)
		{
			throw;
		}
    }
}