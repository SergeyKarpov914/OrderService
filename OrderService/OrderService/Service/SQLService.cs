using Microsoft.Extensions.Configuration;
using OrderService.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace OrderService.Service
{
	public class SQLService
	{
		private readonly IConfiguration _configuration;

		public SQLService(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public async Task<IEnumerable<T>> GetAll<T>(string query) where T : class, ISettable, new()
		{
			DataTable dataTable = new DataTable();
			string connectionString = _configuration.GetConnectionString("Sql");

			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				
				using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
				{
					await Task.Run(() => adapter.Fill(dataTable));
				}
			}
			List<T> data = new List<T>();

			foreach (DataRow row in dataTable.Rows)
			{
				data.Add(new T().SetFrom(row) as T);
			}
			return data;
		}
	}
}
