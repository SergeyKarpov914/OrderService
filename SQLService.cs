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
		public async Task<IEnumerable<T>> GetAll<T>(string query, string connectionString) where T : class, ISettable, new()
		{
			List<T> data = new List<T>();
			DataTable dataTable = new DataTable();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();

					using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
					{
						await Task.Run(() => adapter.Fill(dataTable));
					}
				}
				foreach (DataRow row in dataTable.Rows)
				{
					data.Add(new T().SetFrom(row) as T);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"ERROR! {ex.Message}");
			}
			Console.WriteLine($"Query yields {data.Count} {typeof(T).Name}(s)");
			return data;
		}
	}
}
