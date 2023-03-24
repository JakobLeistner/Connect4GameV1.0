using _4WinGame.BusinessLogic;
using _4WinGame.BusinessLogic.Contracts.Interfaces;
using _4WinGame.RESTApi.Hubs;
using _4WinGame.RESTApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4WinGame.RESTApi
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
            services.AddControllers();
            ConnectionService connectionService = new ConnectionService();
            services.AddSingleton(connectionService);
            IFourWinGamesService fourWinGamesService = new FourWinGamesService();
            FourWinGameEventHandler fourWinGameEventHandler = new FourWinGameEventHandler(connectionService);
            fourWinGamesService.OnGameStarted += fourWinGameEventHandler.OnGameStarted;
            fourWinGamesService.OnWaitingListUpdated += fourWinGameEventHandler.OnWaitingListUpdated;
            services.AddSingleton(fourWinGamesService);
            services.AddSingleton(fourWinGameEventHandler);
            
            services.AddSignalR();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "_4WinGame.RESTApi", Version = "v1" });
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed((host) => true)
                           .AllowCredentials();
                }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_4WinGame.RESTApi v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<RTPHub>("/fourwingamehub");
                endpoints.MapControllers();
            });

            serviceProvider.GetService<FourWinGameEventHandler>().EventHandlerInitalize(configuration["HubUrl"]);

        }
    }
}
