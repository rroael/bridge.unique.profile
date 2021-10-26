using System;
using System.IO;
using System.Reflection;
using Bridge.Commons.System.AspNet.Extensions;
using Bridge.Commons.System.AspNet.Information;
using Bridge.Commons.System.AspNet.Middlewares;
using Bridge.Unique.Profile.API.Extensions;
using Bridge.Unique.Profile.Domain.Contexts.Contracts;
using Bridge.Unique.Profile.IOC;
using Bridge.Unique.Profile.Postgres.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using SimpleInjector.Lifestyles;

namespace Bridge.Unique.Profile.API
{
#pragma warning disable CS1591
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Container.Init();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appInfo = Configuration.GetSection("AppInfo").Get<OpenApiInfo>();

            services.AddLogging();

            services.AddCustomRouting();

            services.AddCustomMvc()
                .AddFluentValidation(fv => { fv.DisableDataAnnotationsValidation = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            Container.InitServices(services);

            //Authentication
            services.AddBupAuthentication();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", appInfo);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var xmlPathCommunication = Path.Combine(AppContext.BaseDirectory, "Bridge.Unique.Profile.Communication.xml");
                c.IncludeXmlComments(xmlPath, true);
                c.IncludeXmlComments(xmlPathCommunication, true);
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.EnvironmentName.Contains("development"))
            {
                var swaggerEndpoint = Configuration.GetSection("SwaggerEndpoint").Get<SwaggerEndpoint>();

                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                app.UseReDoc(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.DocumentTitle = swaggerEndpoint.Name;
                    c.SpecUrl = swaggerEndpoint.Url;
                    c.HideHostname();
                    c.ExpandResponses("200,204");
                    c.RequiredPropsFirst();
                    c.NoAutoAuth();
                    c.SortPropsAlphabetically();
                    c.HideLoading();
                });
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();

            //Logging
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console(new CompactJsonFormatter()) //Serilog, CloudWatch logs the console logs.
                .CreateLogger();

            //Logging
            loggerFactory.AddSerilog();
            //Registers
            Container.InitRegisters(loggerFactory);

            Log.Logger.Warning($"Initializing the application: {DateTime.UtcNow:yyyy-MM-dd.HH:mm:ss}");

            Container.InitApplication();

            //Migração DB
            MigrateDatabase();

            app.UseMiddleware<ErrorHandlingMiddleware>(Log.Logger);

            //app.UseMvc();
            //Substitui app.UseMvc();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void MigrateDatabase()
        {
            using (var scope = AsyncScopedLifestyle.BeginScope(Container.GetContainer()))
            {
                //Migração do banco de dados
                var context = (BupWriteContext)scope.GetInstance<IBupWriteContext>();
                context.Database.Migrate();
            }
        }
    }
#pragma warning restore CS1591
}