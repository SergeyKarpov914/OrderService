using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Interface
{
	internal interface IServiceProcessor
	{
		IServiceProcessor Initialize(IConfiguration configuration);

		Task<IEnumerable<T>> GetAll<T>(string key = null) where T : class, ISettable, new();

		Task<T> Get<T>(string key) where T : class;
		Task<T> Post<T>(T resource) where T : class;
		Task<T> Delete<T>(T resource) where T : class;
	}
}
