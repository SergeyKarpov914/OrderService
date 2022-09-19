using Microsoft.Extensions.Configuration;
using OrderService.Data;
using OrderService.Interface;
using OrderService.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Processor
{
	public class OrderProcessor : IServiceProcessor
	{
		private IConfiguration _configuration;

		public OrderProcessor(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		#region IServiceProcessor

		public async Task<IEnumerable<T>> GetAll<T>(string key = null) where T : class, ISettable, new()
		{
			string query = findQuery<T>(key);

			IEnumerable<T> resultSet = await new SQLService(_configuration).GetAll<T>(query);

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
			string queryKey = null;

			if (typeof(T) == typeof(User))
			{
				queryKey = "Users";
			}
			if (typeof(T) == typeof(Order))
			{
				queryKey = "Orders"; // key is int, dont modify with quotes
			}
			return _configuration.GetValue<string>(queryKey) + key ?? String.Empty;
		}

		#endregion helper(s)
	}
}
