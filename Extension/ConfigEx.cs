using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Extension
{
	public static class ConfigEx
	{
		public static int Port(this IConfiguration cmdLine)
		{
			int port = 0;

			IEnumerable<KeyValuePair<string, string>> cmdPairs = cmdLine.AsEnumerable();

			KeyValuePair<string, string> portPair = cmdPairs.FirstOrDefault(x => x.Key == "p" || x.Key == "port");
			Int32.TryParse(portPair.Value, out port);

			return port;
		}
	}
}
