using OrderService.Interface;
using System.Data;

namespace OrderService.Data
{
	public class Territory : ISettable
	{
		public int    EmployeeID            { get; set; }
		public string TerritoryID			{ get; set; }
		public string TerritoryDescription	{ get; set; }
		public string RegionDescription	    { get; set; }

		public ISettable SetFrom(DataRow row)
		{
			EmployeeID           = row.Field<int   >(nameof(EmployeeID          ));
			TerritoryID          = row.Field<string>(nameof(TerritoryID         )).Trim();
			TerritoryDescription = row.Field<string>(nameof(TerritoryDescription)).Trim(); ;
			RegionDescription    = row.Field<string>(nameof(RegionDescription   )).Trim(); ;

			return this;
		}
	}
}
