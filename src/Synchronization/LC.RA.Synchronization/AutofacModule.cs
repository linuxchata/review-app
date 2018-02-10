﻿using Autofac;
using LC.RA.Synchronization.Core.Application;
using LC.RA.Synchronization.Services;
using Microsoft.Extensions.Configuration;

namespace LC.RA.Synchronization
{
    public sealed class AutofacModule : Module
    {
        private readonly IConfiguration configuration;

        public AutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var applicationSettings = new ApplicationSettings();
            this.configuration.GetSection("Settings").Bind(applicationSettings);
            builder.RegisterInstance(applicationSettings)
                .AsImplementedInterfaces();

            this.RegisterServices(builder);
        }
        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<WikipediaService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<WikipediaParsingService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationSynchronizationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}