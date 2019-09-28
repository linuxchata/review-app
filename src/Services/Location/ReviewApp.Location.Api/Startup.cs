
using Autofac;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

using ReviewApp.HealthChecks;

namespace ReviewApp.Location.Api
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

            services.AddMvc();
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

            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}
