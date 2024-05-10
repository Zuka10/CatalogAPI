using Catalog.API.Models;
using Catalog.Domain;
using Catalog.Facade.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		try
		{
			var response = await _orderService.GetByIdAsync(id);
			if (response is null)
				return NotFound();

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
			var order = new Order
			{
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
			return Ok(order);
		}
		catch (Exception)
		{
			throw;
		}
    }
}