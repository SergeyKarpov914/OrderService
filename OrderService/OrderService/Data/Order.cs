using OrderService.Interface;
using System;
using System.Data;
using System.Net;

namespace OrderService.Data
{
	internal class Order : ISettable
	{
		public int      OrderID        { get; set; }
		public int      EmployeeID     { get; set; }
		public int      ShipVia        { get; set; }
		public double   Freight        { get; set; }
		public string   CustomerID     { get; set; }
		public DateTime OrderDate      { get; set; }
		public DateTime RequiredDate   { get; set; }
		public DateTime ShippedDate    { get; set; }
		public string   ShipName       { get; set; }
		public string   ShipAddress    { get; set; }
		public string   ShipCity       { get; set; }
		public string   ShipRegion     { get; set; }
		public string   ShipPostalCode { get; set; }
		public string   ShipCountry    { get; set; }

		public ISettable SetFrom(DataRow row)
		{
			OrderID        = row.Field<int>     (nameof(OrderID));
			EmployeeID     = row.Field<int>     (nameof(EmployeeID));
			ShipVia        = row.Field<int>     (nameof(ShipVia));
			Freight        = row.Field<double>  (nameof(Freight));
			CustomerID     = row.Field<string>  (nameof(CustomerID));
			OrderDate      = row.Field<DateTime>(nameof(OrderDate));
			RequiredDate   = row.Field<DateTime>(nameof(RequiredDate));
			ShippedDate    = row.Field<DateTime>(nameof(ShippedDate));
			ShipName       = row.Field<string>  (nameof(ShipName));
			ShipAddress    = row.Field<string>  (nameof(ShipAddress));
			ShipCity       = row.Field<string>  (nameof(ShipCity));
			ShipRegion     = row.Field<string>  (nameof(ShipRegion));
			ShipPostalCode = row.Field<string>  (nameof(ShipPostalCode));
			ShipCountry    = row.Field<string>  (nameof(ShipCountry));

			return this;
		}
	}
}


