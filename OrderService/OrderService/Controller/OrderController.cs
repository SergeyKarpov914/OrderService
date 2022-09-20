using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderService.Data;
using OrderService.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Controller
{
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IServiceProcessor _processor;

		public OrderController(IServiceProcessor processor)
		{ 
			_processor = processor ?? throw new ArgumentNullException(nameof(processor));
		}

		[Route("api/orders/{key}")]
		[HttpGet]
		public async Task<IActionResult> GetOrders(string key)
		{
			IEnumerable<Order> orders = null;
			try
			{
				orders = await _processor.GetAll<Order>(key);
			}
			catch //(Exception ex)
			{
				return BadRequest();
			}
			return Ok(orders);
		}

		[Route("api/users")]
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			IEnumerable<User> users = null;
			try
			{
				users = await _processor.GetAll<User>();
			}
			catch //(Exception ex)
			{
				return BadRequest();
			}
			return Ok(users);
		}
	}
}
