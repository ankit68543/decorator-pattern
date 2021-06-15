using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using decorator_design_pattern.Contract;
using decorator_design_pattern.Service;
using decorator_pattern.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace decorator_pattern
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMemoryCache(); 
        //    services.AddScoped<PlayersService>();
        //    services.AddScoped(serviceProvider =>
        //    {
        //        var memoryCache = serviceProvider.GetService<IMemoryCache>();
        //        var logger = serviceProvider.GetService<ILogger<PlayersServiceLoggingDecorator>>();

        //        var playerService = serviceProvider.GetRequiredService<PlayersService>();

        //        IPlayersService cachingDecorator = new PlayersServiceCachingDecorator(playerService, memoryCache);
        //        IPlayersService loggingDecorator = new PlayersServiceLoggingDecorator(cachingDecorator, logger);

        //        return loggingDecorator;
        //    });

        //    services.AddControllers();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IPlayersService,PlayersService>();

            if (Convert.ToBoolean(Configuration["EnableCaching"]))
            {
                services.Decorate<IPlayersService, PlayersServiceCachingDecorator>();
            }

            if (Convert.ToBoolean(Configuration["EnableLogging"]))
            {
                services.Decorate<IPlayersService, PlayersServiceLoggingDecorator>();
            }

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
