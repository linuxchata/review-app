using Autofac;
using LC.RA.Web.Core.Application;
using LC.RA.Web.DataAccess;
using LC.RA.Web.DataAccess.Converters;
using LC.RA.Web.Services;
using LC.ServiceBusAdapter;
using Microsoft.Extensions.Configuration;

namespace LC.RA.Web.Api
{
    /// <summary>
    /// Autofac module
    /// </summary>
    public class AutofacModule : Module
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

            builder.RegisterType<DatabaseConnection>()
                .WithParameter("connectionString", applicationSettings.ConnectionString)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterInstance(applicationSettings)
                .AsImplementedInterfaces();

            this.RegisterConverters(builder);

            this.RegisterRepositories(builder);

            this.RegisterServices(builder, applicationSettings);
        }

        private void RegisterConverters(ContainerBuilder builder)
        {
            builder.RegisterType<LocationConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<SpecializationConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<DoctorConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<Services.Converters.LocationsConverter>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<LocationRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<SpecializationRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<DoctorRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private void RegisterServices(ContainerBuilder builder, IApplicationSettings applicationSettings)
        {
            builder.RegisterType<LocationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<SpecializationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<SubjectService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<WebApiMessageHandler>()
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