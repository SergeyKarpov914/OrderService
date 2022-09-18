using System.Data;

namespace OrderService.Interface
{
	internal interface ISettable
	{
		ISettable SetFrom(DataRow row); 
	}
}
