using System;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using NLog.Web;

namespace ReviewApp.Location.Api
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Building web host for LC.RA.Location.Api");

                CreateWebHostBuilder(args).Build().Run();

                logger.Info("Web host for LC.RA.Location.Api has been built");
            }
            catch (Exception e)
            {
                logger.Error(e, "LC.RA.Location.Api application initialization exception");
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
                .UseApplicationInsights()
                .ConfigureServices(s => s.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>();
    }
}
