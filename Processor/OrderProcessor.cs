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

		public OrderProcessor(IConfiguration config)
		{
			configuration = config ?? throw new ArgumentNullException(nameof(configuration));

			if (string.IsNullOrEmpty(connectionString))
			{
				Console.WriteLine($"Set statics [{Thread.CurrentThread.ManagedThreadId}]");
				
				connectionString = configuration.GetConnectionString("Sql");
				userQuery        = configuration.GetValue<string>("Users");
				orderQuery       = configuration.GetValue<string>("Orders");
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
				return orderQuery + key;
			}
			throw new Exception($"{typeof(T).Name} is unsupported type");
		}

		#endregion helper(s)
	}
}
