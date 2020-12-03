using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataContext.Web.Data;

namespace DataContext.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();


			#region Data Protection
			// Connect to redis
			var redisConnection = ConnectionMultiplexer.Connect(GetRedisConfiguration());
			//Refer to SetApplicationName in the following link https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-3.1
			services.AddDataProtection()
				.SetApplicationName(Configuration.GetValue<string>(Constants.AppNamePath))
				.PersistKeysToStackExchangeRedis(redisConnection, Configuration.GetValue<string>(Constants.DataProtectionFilePath));

			//services.AddDbContext<DataContextWebContext>(options =>
			//        options.UseSqlServer(Configuration.GetConnectionString("DataContextWebContext")));
			services.AddDbContext<DataContextWebContext>(options =>
					options.UseInMemoryDatabase(databaseName:"DataContextTest")); //Use only for testing/sample apps
			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}


		private ConfigurationOptions GetRedisConfiguration()
		{
			return new ConfigurationOptions
			{
				AbortOnConnectFail = Configuration.GetValue<bool>(Constants.RedisConfigPath.AbortOnConnectFail),
				Ssl = Configuration.GetValue<bool>(Constants.RedisConfigPath.UseSsl),
				ConnectRetry = Configuration.GetValue<int>(Constants.RedisConfigPath.ConnectRetry),
				ConnectTimeout = Configuration.GetValue<int>(Constants.RedisConfigPath.ConnectTimeout),
				SyncTimeout = Configuration.GetValue<int>(Constants.RedisConfigPath.SyncTimeout),
				DefaultDatabase = Configuration.GetValue<int>(Constants.RedisConfigPath.DefaultDatabase),
				EndPoints = { { Configuration.GetValue<string>(Constants.RedisConfigPath.Dns), Configuration.GetValue<int>(Constants.RedisConfigPath.Port) } },
				Password = Configuration.GetValue<string>(Constants.RedisConfigPath.Password)
			};
		}
	}
}
