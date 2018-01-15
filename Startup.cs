using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartUnitApi.WebSocketServices;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SmartUnitApi.Entities;
using Microsoft.EntityFrameworkCore;
using SmartUnitApi.Repositories;
using SmartUnitApi.WebSocketChannels;

namespace SmartUnitApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebSocketManager();
            services.AddEntityFramework()
                .AddDbContext<SmartUnitDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SmartUnitDb")));
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISensorRepository, SensorRepository>();
            services.AddTransient<IActuatorRepository, ActuatorRepository>();
            services.AddTransient<IMunicipalityRepository, MunicipalityRepository>();
            services.AddTransient<IAlarmRepository, AlarmRepository>();
            services.AddTransient<ICountyRepository, CountyRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ApiName = "smartunitapi"
            });
            app.UseWebSockets();
            app.MapWebSocketManager("/ws", serviceProvider.GetService<ChatMessageHandler>());
            //app.MapWebSocketManager("/test", serviceProvider.GetService<TestMessageHandler>());
            app.UseStaticFiles();
            app.UseMvc();

        }
    }
}
