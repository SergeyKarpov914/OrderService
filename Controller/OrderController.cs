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

		[Route("api/orders")]
		[HttpGet]
		public async Task<IActionResult> GetAllOrders()
		{
			IEnumerable<Order> orders = null;
			try
			{
				orders = await _processor.GetAll<Order>() ?? throw new Exception($"NULL {typeof(Order).Name} collection returned by processor");
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetAllOrders), ex.Message);
			}
			return this.Success(orders, nameof(GetAllOrders));
		}

		[Route("api/products/{key}")]
		[HttpGet]
		public async Task<IActionResult> GetProducts(string key)
		{
			IEnumerable<Product> products = null;
			try
			{
				products = await _processor.GetAll<Product>(key) ?? throw new Exception($"NULL {typeof(Product).Name} collection returned by processor");
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetProducts), ex.Message);
			}
			return this.Success(products, nameof(GetProducts));
		}

		[Route("api/territories/{key}")]
		[HttpGet]
		public async Task<IActionResult> GetTerritories(string key)
		{
			IEnumerable<Territory> territories = null;
			try
			{
				territories = await _processor.GetAll<Territory>(key) ?? throw new Exception($"NULL {typeof(Territory).Name} collection returned by processor");
			}
			catch (Exception ex)
			{
				return this.Failed(nameof(GetTerritories), ex.Message);
			}
			return this.Success(territories, nameof(GetTerritories));
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
				return this.Failed(nameof(GetUsers), ex.Message);
			}
			return this.Success(users, nameof(GetUsers));
		}
	}
}
