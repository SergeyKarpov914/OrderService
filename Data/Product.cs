using Microsoft.Extensions.Hosting;
using OrderService.Interface;
using System.Data;

namespace OrderService.Data
{
	public class Product : ISettable
	{
		public int     OrderID         { get; set; }
		public string  ProductName     { get; set; }
		public decimal OrderUnitPrice  { get; set; }
		public short   Quantity        { get; set; }
		public float   Discount        { get; set; }
		public string  QuantityPerUnit { get; set; }
		public decimal UnitPrice       { get; set; }
		public short   UnitsInStock    { get; set; }
		public short   UnitsOnOrder    { get; set; }
		public short   ReorderLevel    { get; set; }
		public bool    Discontinued    { get; set; }
		public string  CompanyName     { get; set; }
		public string  ContactName     { get; set; }
		public string  ContactTitle    { get; set; }
		public string  Address         { get; set; }
		public string  City            { get; set; }
		public string  Region          { get; set; }
		public string  PostalCode      { get; set; }
		public string  Country         { get; set; }
		public string  Phone           { get; set; }
		public string  Fax             { get; set; }
		public string  HomePage        { get; set; }

		public ISettable SetFrom(DataRow row)
		{
			OrderID         = row.Field<int    >(nameof(OrderID));
			ProductName     = row.Field<string >(nameof(ProductName));
			OrderUnitPrice  = row.Field<decimal>(nameof(OrderUnitPrice));
			Quantity        = row.Field<short  >(nameof(Quantity));
			Discount        = row.Field<float  >(nameof(Discount));
			QuantityPerUnit = row.Field<string >(nameof(QuantityPerUnit));
			UnitPrice       = row.Field<decimal>(nameof(UnitPrice));
			UnitsInStock    = row.Field<short  >(nameof(UnitsInStock));
			UnitsOnOrder    = row.Field<short  >(nameof(UnitsOnOrder));
			ReorderLevel    = row.Field<short  >(nameof(ReorderLevel));
			Discontinued    = row.Field<bool   >(nameof(Discontinued));
			CompanyName     = row.Field<string >(nameof(CompanyName));
			ContactName     = row.Field<string >(nameof(ContactName));
			ContactTitle    = row.Field<string >(nameof(ContactTitle));
			Address         = row.Field<string >(nameof(Address));
			City            = row.Field<string >(nameof(City));
			Region          = row.Field<string >(nameof(Region));
			PostalCode      = row.Field<string >(nameof(PostalCode));
			Country         = row.Field<string >(nameof(Country));
			Phone           = row.Field<string >(nameof(Phone));
			Fax             = row.Field<string >(nameof(Fax));
			HomePage        = row.Field<string>(nameof(HomePage));

			return this;
		}
	}
}
