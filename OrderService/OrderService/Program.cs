using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace OrderService
{
	public class Program
	{
		private static IWebHost host;
		private static IConfigurationRoot config;

		static void Main(string[] args)
		{
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true)
						                            .AddCommandLine(args)  							
					                                .Build();

				int port = findPort(config); // command line convention is: key=value , where key is clean string with no '-'

				host = new WebHostBuilder().UseConfiguration(config)
											.ConfigureAppConfiguration(builder =>
											{
												builder.AddCommandLine(args);
											})
											.UseKestrel(opts =>
											{
												opts.Listen(IPAddress.Loopback, port);
												opts.ListenAnyIP(port);
											})
											.UseContentRoot(Directory.GetCurrentDirectory())
											.UseStartup<Startup>()
											.UseIIS()
											.Build();

				Console.WriteLine($"Server hosting with Kestrel: {Dns.GetHostEntry(Dns.GetHostName()).HostName}:{port}");
				
				host.Run();
			}
			catch //(Exception ex)
			{
			}
		}

		private static int findPort(IConfiguration cmdLine)
		{
			int port = 0;
			
			IEnumerable<KeyValuePair<string, string>> cmdPairs = cmdLine.AsEnumerable();

			KeyValuePair<string, string> portPair = cmdPairs.FirstOrDefault(x => x.Key == "p" || x.Key == "port");
			Int32.TryParse(portPair.Value, out port);

			Console.WriteLine($"Port {port} set from command line config");
			return port;
		}
	}
}
