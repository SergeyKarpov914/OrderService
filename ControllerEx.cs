using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;

namespace OrderService.Extension
{
	public static class ControllerEx
	{
		public static OkObjectResult Success(this ControllerBase controller, object data, string context)
		{
			OkObjectResult result = controller.Ok(data);

			string count = null;

			if (data is IList list)
			{
				count = $"{list.Count} items";
			}
			Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss,fff")}] {context} returns {count}, code '{result.StatusCode}'");
			return result;
		}
		public static BadRequestResult Failed(this ControllerBase controller, string context, string error)
		{
			BadRequestResult result = controller.BadRequest();

			Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss,fff")}] {context} failed, code '{result.StatusCode}' ({error})");
			return result;
		}
	}
}
