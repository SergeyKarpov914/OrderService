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
		private static IConfiguration configuration;

		private static string connectionString;

		private static string userQuery;
		private static string orderQuery;
		private static string productQuery;
		private static string territoryQuery;
		private static string userKey;
		private static string orderKey;

		public OrderProcessor(IConfiguration config)
		{
			configuration = config ?? throw new ArgumentNullException(nameof(configuration));

			if (string.IsNullOrEmpty(connectionString))
			{
				Console.WriteLine($"Set statics [{Thread.CurrentThread.ManagedThreadId}]");
				
				connectionString = configuration.GetConnectionString("Sql");

				userQuery      = configuration.GetValue<string>("Users");
				orderQuery     = configuration.GetValue<string>("Orders");
				productQuery   = configuration.GetValue<string>("Products");
				territoryQuery = configuration.GetValue<string>("Territories");
				orderKey       = configuration.GetValue<string>("OrderKey");
				userKey        = configuration.GetValue<string>("UserKey");
			}
		}

		#region IServiceProcessor

		public async Task<IEnumerable<T>> GetAll<T>(string key = null) where T : class, ISettable, new()
		{
			string query = findQuery<T>(key);
			Console.WriteLine($"GetAll [{Thread.CurrentThread.ManagedThreadId}] '{key}' '{query}'");

			IEnumerable<T> resultSet = await new SQLService().GetAll<T>(query, connectionString);
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

		private string findQuery<T>(string key = null)
		{
			if (typeof(T) == typeof(User))
			{
				return userQuery;
			}
			if (typeof(T) == typeof(Order))
			{
				return orderQuery + clause(userKey, key);
			}
			if (typeof(T) == typeof(Product))
			{
				return productQuery + clause(orderKey, key); 
			}
			if (typeof(T) == typeof(Territory))
			{
				return territoryQuery + clause(userKey, key); 
			}
			throw new Exception($"{typeof(T).Name} is unsupported type");
		}

		private string clause(string key, string value)
		{
			if (null != value)
			{
				return $" WHERE {key}={value}";
			}
			return string.Empty;
		}

		#endregion helper(s)
	}
}
