using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OrderService.Extension;
using System;
using System.IO;
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

				int port = _config.Port(); 

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

				Console.WriteLine($"Port {port} set from command line config");
				Console.WriteLine($"Server hosting with Kestrel: {Dns.GetHostEntry(Dns.GetHostName()).HostName}:{port}");
				
				_host.Run();
			}
			catch //(Exception ex)
			{
			}
		}
	}
}
