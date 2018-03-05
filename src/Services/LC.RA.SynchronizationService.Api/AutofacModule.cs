﻿using Autofac;
using LC.RA.SynchronizationService.Api.Infrastructure.Converters;
using LC.RA.SynchronizationService.Api.Infrastructure.Services;
using LC.RA.SynchronizationService.Api.Model.Application;
using LC.ServiceBusAdapter;
using Microsoft.Extensions.Configuration;

namespace LC.RA.SynchronizationService.Api
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

            this.RegisterServices(builder, applicationSettings);
        }

        private void RegisterServices(ContainerBuilder builder, IApplicationSettings applicationSettings)
        {
            builder.RegisterType<WikipediaService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<WikipediaParsingService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<Infrastructure.Services.LocationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationServiceQueueMessageHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationsConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueueMessageSenderService>()
                .WithParameter("connectionString", applicationSettings.ServiceBusConnectionString)
                .WithParameter("queueName", applicationSettings.WebApiQueueName)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<QueueMessageReceiverService>()
                .WithParameter("connectionString", applicationSettings.ServiceBusConnectionString)
                .WithParameter("queueName", applicationSettings.LocationServiceQueueName)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}