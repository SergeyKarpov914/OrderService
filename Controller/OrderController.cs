using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Extension;
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
				orders = await _processor.GetAll<Order>(key) ?? throw new Exception($"NULL {typeof(Order).Name} collection returned by processor");
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetOrders), ex.Message);
			}
			return this.Success(orders, nameof(GetOrders));
		}

		[Route("api/users")]
		[HttpGet]
		public async Task<IActionResult> GetUsers()
		{
			IEnumerable<User> users = null;
			try
			{
				users = await _processor.GetAll<User>() ?? throw new Exception($"NULL {typeof(User).Name} collection returned by processor"); ;
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetOrders), ex.Message);
			}
			return this.Success(users, nameof(GetOrders));
		}
	}
}
