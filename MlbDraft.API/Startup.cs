using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MLBDraft.API.Repositories;
using MLBDraft.API.Entities;
using MLBDraft.API.Models;
using MLBDraft.API.Converters;
using MLBDraft.API.Security;
using MLBDraft.API.Middleware;
using MLBDraft.API.Helpers;
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
        {;

            services.AddMvc(); //SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpContextAccessor();
 
            //register db
             var connectionString = Configuration["connectionStrings:mlbDraftDBConnectionString"];
             services.AddDbContext<MLBDraftContext>
                (options => options.UseSqlite(connectionString));

            // register Identity 
            services.AddIdentity<MlbDraftUser, IdentityRole>()
                .AddEntityFrameworkStores<MLBDraftContext>()
                .AddDefaultTokenProviders();

            

            //register bearer token authentication 
            var secretKey = Configuration.GetSection("Tokens:Key").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            services.AddAuthentication(o => {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters(){
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetSection("Tokens:Issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetSection("Tokens:Audience").Value,
                        ValidateLifetime = false
                   };
                });

            //register authorization policy
            services.AddAuthorization(cfg => {
                cfg.AddPolicy("MlbDraftUsers", p => p.RequireClaim("MlbDraftUser", "True"));
            });

            //register cors policy
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("MlbDraftCors", bldr => { 
                    bldr.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();  //Restrict origin on load to Prod env.
                });
            });
                
             //add TokenGeneratorOptions object to configuration -- used by TokenGenerator
            services.Configure<TokenGeneratorOptions>(options => {
                options.Audience = Configuration.GetSection("Tokens:Audience").Value;
                options.Issuer = Configuration.GetSection("Tokens:Issuer").Value;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            //register the repository
            services.AddScoped<IMlbDraftRepository, MlbDraftRepository>();
            services.AddScoped<IMlbTeamRepository, MlbTeamRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IDraftRepository, DraftRepository>();
            services.AddScoped<IDraftSelectionRepository, DraftSelectionRepository>();
            services.AddScoped<IDraftTeamRosterRepository, DraftTeamRosterRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            //register security services
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IMlbDraftIdentityInitializer, MlbDraftIdentityInitializer>();

            //Add UrlHelper (to create prev/next paging links  for X-Pagination header)
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });

            //Add AutoMapper with profile
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
        MLBDraftContext mlbDraftContext,
        IMlbDraftIdentityInitializer mlbDraftIdentityInitializer)
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

            //Global error handling
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
            app.UseAuthentication(); //Uses jwt authentication
            app.UseMiddleware<DeChunkerMiddleware>();

            mlbDraftIdentityInitializer.Seed().Wait();
            mlbDraftContext.EnsureSeedDataForContext();

             
            app.UseMvc();


            
        }
    }
}
