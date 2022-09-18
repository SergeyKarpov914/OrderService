using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Interface;
using System;

namespace OrderService
{
	internal class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			try
			{
				services.AddAuthentication(IISDefaults.AuthenticationScheme);
				services.AddControllers();
				services.AddTransient<IServiceProcessor>(provider => (provider.GetService(typeof(IServiceProcessor)) as IServiceProcessor)?.Initialize(Configuration));
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
