using System;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using NLog.Web;

using ReviewApp.Api.Infrastructure.Extensions;

namespace ReviewApp.Web.Api
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        private const string AspnetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Entry point of the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Building and running web host for ReviewApp.Web.Api");

                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "ReviewApp.Web.Api application initialization exception");
                throw;
            }
        }

        /// <summary>
        /// Create web host builder
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Created web host builder</returns>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(a => KestrelOptionsExtension.ConfigureHttps(a, GetConfiguration()))
                .ConfigureServices(s => s.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>();

        private static IConfiguration GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable(AspnetCoreEnvironment);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
