using Autofac;
using LC.RA.Synchronization.Api.Infrastructure.Converters;
using LC.RA.Synchronization.Api.Infrastructure.Services;
using LC.RA.Synchronization.Api.Models.Application;
using LC.ServiceBusAdapter;
using Microsoft.Extensions.Configuration;

namespace LC.RA.Synchronization.Api
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
            builder.RegisterType<LocationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationServiceMessageHandler>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<LocationsConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<TopicSenderService>()
                .WithParameter("connectionString", applicationSettings.ServiceBusConnectionString)
                .WithParameter("topicName", applicationSettings.ServiceBusTopicName)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<TopicReceiverService>()
                .WithParameter("connectionString", applicationSettings.ServiceBusConnectionString)
                .WithParameter("topicName", applicationSettings.ServiceBusTopicName)
                .WithParameter("subscriptionName", applicationSettings.ServiceBusSubscriptionName)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}