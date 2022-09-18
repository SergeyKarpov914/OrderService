using OrderService.Interface;
using System.Data;

namespace OrderService.Data
{
	internal class User : ISettable
	{
		public int    EmployeeID { get; set; }
		public string LastName   { get; set; }
		public string FirstName  { get; set; }
		public string Title      { get; set; }

		public ISettable SetFrom(DataRow row)
		{
			EmployeeID = row.Field<int>   (nameof(EmployeeID));
			LastName   = row.Field<string>(nameof(LastName));
			FirstName  = row.Field<string>(nameof(FirstName));
			Title      = row.Field<string>(nameof(Title));
		      
			return this;
		}
	}
}
