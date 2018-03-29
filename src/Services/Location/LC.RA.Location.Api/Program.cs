using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace LC.RA.Location.Api
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
                logger.Info("Building web host for LocationService");

                BuildWebHost(args).Run();

                logger.Info("Web host for LocationService has been built");
            }
            catch (Exception e)
            {
                logger.Error(e, "LocationService application initialization exception");
                throw;
            }
        }

        /// <summary>
        /// Build web host
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>Created web host</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .ConfigureServices(s => s.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>()
                .Build();
    }
}
