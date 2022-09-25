using System.Data;

namespace OrderService.Interface
{
	public interface ISettable
	{
		ISettable SetFrom(DataRow row); 
	}
}
