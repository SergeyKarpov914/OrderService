using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Extension;
using OrderService.Interface;
using OrderService.Processor;
using System;

namespace OrderService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; private set; }

		private const string AllowOrigins = "AllowOrigins";

		public void ConfigureServices(IServiceCollection services)
		{
			try
			{
				services.AddCors(options =>
				{
					options.AddPolicy(name: AllowOrigins,
									  policy =>
									  {
										  policy.AllowAnyOrigin()
												.AllowAnyHeader()
												.AllowAnyMethod();
									  });
				});
				services.AddAuthentication(IISDefaults.AuthenticationScheme);
				services.AddControllers();
				services.AddTransient<IServiceProcessor, OrderProcessor>();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			try
			{
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}
				app.UseCors(AllowOrigins);
				app.UseRouting();
  			    app.UseAuthorization();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
