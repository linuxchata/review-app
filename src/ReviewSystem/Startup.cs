using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReviewSystem
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjection.Intiailize(services, this.configuration);

            services.AddMvc();
            services.Configure<ApplicationSettings>(this.configuration.GetSection("Settings"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    // Disable caching for all static files.
                    context.Context.Response.Headers["Cache-Control"] = configuration["StaticFiles:Headers:Cache-Control"];
                    context.Context.Response.Headers["Pragma"] = configuration["StaticFiles:Headers:Pragma"];
                    context.Context.Response.Headers["Expires"] = configuration["StaticFiles:Headers:Expires"];
                }
            });
            app.UseStatusCodePages();

            app.UseMvc();
        }
    }
}
