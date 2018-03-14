using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace LC.RA.Location.Api
{
    public class Program
    {
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

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .ConfigureServices(s => s.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>()
                .Build();
    }
}
