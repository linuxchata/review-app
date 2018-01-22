using Autofac;
using Microsoft.Extensions.Configuration;
using ReviewSystem.DataAccess;
using ReviewSystem.DataAccess.Converters;
using ReviewSystem.Services;
using ReviewSystem.Services.Synchronization;

namespace ReviewSystem
{
    public class AutofacModule : Module
    {
        private readonly IConfiguration configuration;

        public AutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = this.configuration.GetValue<string>("Settings:ConnectionString");

            builder.RegisterType<DatabaseConnection>()
                .WithParameter("connectionString", connectionString)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            this.RegisterConverters(builder);

            this.RegisterRepositories(builder);

            this.RegisterServices(builder);
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

        private void RegisterServices(ContainerBuilder builder)
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
            builder.RegisterType<LocationSynchronizationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}