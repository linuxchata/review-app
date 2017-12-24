using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewSystem.DataAccess;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem
{
    public static class DependencyInjection
    {
        public static void Intiailize(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Settings:ConnectionString");

            services.AddTransient<IDatabaseConnection>(_ => new DatabaseConnection(connectionString));

            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();

            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<ISubjectService, SubjectService>();
        }
    }
}