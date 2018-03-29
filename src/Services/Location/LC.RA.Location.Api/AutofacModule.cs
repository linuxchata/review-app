using Autofac;
using LC.RA.Location.Core.Application;
using LC.RA.Location.Infrastructure.Converters;
using LC.RA.Location.Infrastructure.Services;
using LC.ServiceBusAdapter;
using Microsoft.Extensions.Configuration;

namespace LC.RA.Location.Api
{
    /// <summary>
    /// Autofac module
    /// </summary>
    public sealed class AutofacModule : Module
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacModule"/> class
        /// </summary>
        /// <param name="configuration"></param>
        public AutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Initialize dependencies
        /// </summary>
        /// <param name="builder"></param>
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