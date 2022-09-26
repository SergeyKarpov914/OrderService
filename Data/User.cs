using OrderService.Interface;
using System;
using System.Data;

namespace OrderService.Data
{
	public class User : ISettable
	{
		public int       EmployeeID      { get; set; }
		public string    LastName        { get; set; }
		public string    FirstName       { get; set; }
		public string    Title           { get; set; }
		public string    TitleOfCourtesy { get; set; }
		public DateTime? BirthDate       { get; set; }
		public DateTime? HireDate        { get; set; }
		public string    Address         { get; set; }
		public string    City            { get; set; }
		public string    Region          { get; set; }
		public string    PostalCode      { get; set; }
		public string    Country         { get; set; }
		public string    HomePhone       { get; set; }
		public string    Extension       { get; set; }
		public string    Notes           { get; set; }
		public string    ReportsTo       { get; set; }
		public string    PhotoPath       { get; set; }

		public ISettable SetFrom(DataRow row)
		{
			EmployeeID      = row.Field<int      >(nameof(EmployeeID));
			LastName        = row.Field<string   >(nameof(LastName));
			FirstName       = row.Field<string   >(nameof(FirstName));
			Title           = row.Field<string   >(nameof(Title));
			TitleOfCourtesy = row.Field<string   >(nameof(TitleOfCourtesy));
			BirthDate       = row.Field<DateTime?>(nameof(BirthDate));
			HireDate        = row.Field<DateTime?>(nameof(HireDate));
			Address         = row.Field<string   >(nameof(Address));
			City            = row.Field<string   >(nameof(City));
			Region          = row.Field<string   >(nameof(Region));
			PostalCode      = row.Field<string   >(nameof(PostalCode));
			Country         = row.Field<string   >(nameof(Country));
			HomePhone       = row.Field<string   >(nameof(HomePhone));
			Extension       = row.Field<string   >(nameof(Extension));
			Notes           = row.Field<string   >(nameof(Notes));
			ReportsTo       = row.Field<string   >(nameof(ReportsTo));
			PhotoPath       = row.Field<string   >(nameof(PhotoPath));

			return this;
		}
	}
}
