using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace LC.RA.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Info("Building Web host");

                BuildWebHost(args).Run();

                logger.Info("Web host has been built");
            }
            catch (Exception e)
            {
                logger.Error(e, "Application initialization exception");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(s => s.AddAutofac())
                .UseNLog()
                .UseStartup<Startup>()
                .Build();
    }
}
