using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MLBDraft.API.Repositories;
using MLBDraft.API.Entities;
using MLBDraft.API.Models;
using MLBDraft.API.Converters;
using AutoMapper;


namespace MLBDraft
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();
 
             var connectionString = Configuration["connectionStrings:mlbDraftDBConnectionString"];
             services.AddDbContext<MLBDraftContext>
                (options => options.UseSqlite(connectionString));

            //register the repository
            services.AddScoped<IMlbDraftRepository, MlbDraftRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();

            //Add AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>{
                mc.AddProfile(new Converter());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
        IHostingEnvironment env, 
        ILoggerFactory loggerFactory, 
        MLBDraftContext mlbDraftContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                
            }

            app.UseExceptionHandler(appBuilder => {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if(exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, 
                            exceptionHandlerFeature.Error,
                            exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await HttpResponseWritingExtensions.WriteAsync(context.Response, "An unexpected error has occurred.");
                        

                    });
                });


            app.UseHttpsRedirection();

            mlbDraftContext.EnsureSeedDataForContext();
            app.UseMvc();
        }
    }
}
