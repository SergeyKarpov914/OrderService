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
		private static IWebHost _host;
		private static IConfigurationRoot _config;

		static void Main(string[] args)
		{
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

				_config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
	    				                            .AddCommandLine(args)  							
					                                .Build();

				int port = findPort(_config); // command line convention is: key=value , where key is clean string with no '-'

				_host = new WebHostBuilder().UseConfiguration(_config)
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
				
				_host.Run();
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
