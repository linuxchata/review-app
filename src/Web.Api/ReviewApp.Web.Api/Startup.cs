using System;
using System.IO;

using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using ReviewApp.HealthChecks;
using ReviewApp.Web.Core.Application;

namespace ReviewApp.Web.Api
{
    /// <summary>
    /// Startup class for the application
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services">Collection of the services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoDbConnectionString = this.configuration.GetSection("Settings:ConnectionString").Value;
            services
                .AddHealthChecks()
                .AddCheck(nameof(MongoDbHealthCheck), new MongoDbHealthCheck(mongoDbConnectionString), HealthStatus.Unhealthy);

            services.AddCors();

            services.AddMvc();

            services.Configure<ApplicationSettings>(this.configuration.GetSection("Settings"));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Review application API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ReviewApp.Web.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configure container
        /// </summary>
        /// <param name="builder">Container builder</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(this.configuration));
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="app">Application builder</param>
        /// <param name="env">Web hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseStatusCodePages();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Review application API V1");
                c.RoutePrefix = "swagger/ui";
            });

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHealthChecks("/health");

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
