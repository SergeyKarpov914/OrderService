using Microsoft.Extensions.Configuration;
using OrderService.Data;
using OrderService.Interface;
using OrderService.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Processor
{
	public class OrderProcessor : IServiceProcessor
	{
		private readonly IConfiguration _configuration;
		private readonly string         _connectionString;

		public OrderProcessor(IConfiguration config)
		{
			_configuration    = config ?? throw new ArgumentNullException(nameof(_configuration));
			_connectionString = _configuration.GetConnectionString("Sql");
		}

		#region IServiceProcessor

		public async Task<IEnumerable<T>> GetAll<T>(string key = null) where T : class, ISettable, new()
		{
			string query = formatQuery<T>(key);
			Console.WriteLine($"GetAll [{Thread.CurrentThread.ManagedThreadId}] '{key}' '{query}'");

			IEnumerable<T> resultSet = await new SQLService().GetAll<T>(query, _connectionString);
			return resultSet;
		}

		public Task<T> Get<T>(string key) where T : class
		{
			throw new NotImplementedException();
		}

		public Task<T> Post<T>(T resource) where T : class
		{
			throw new NotImplementedException();
		}

		public Task<T> Delete<T>(T resource) where T : class
		{
			throw new NotImplementedException();
		}

		#endregion IServiceProcessor

		#region helper(s)

		private string formatQuery<T>(string what = null)
		{
			string query = null;
			string key   = null;

			if (typeof(T) == typeof(User))
			{
				query = _configuration.GetValue<string>("Users");
			}
			else if (typeof(T) == typeof(Order))
			{
				query = _configuration.GetValue<string>("Orders");
				key   = _configuration.GetValue<string>("UserKey");
			}
			else if (typeof(T) == typeof(Product))
			{
				query = _configuration.GetValue<string>("Products");
				key   = _configuration.GetValue<string>("OrderKey");
			}
			else if (typeof(T) == typeof(Territory))
			{
				query = _configuration.GetValue<string>("Territories");
				key   = _configuration.GetValue<string>("UserKey");
			}
			else
			{
				throw new Exception($"{typeof(T).Name} is unsupported type");
			}
			if (null != what)
			{
				return $"{query} WHERE {key}={what}";
			}
			return query;
		}

		#endregion helper(s)
	}
}
