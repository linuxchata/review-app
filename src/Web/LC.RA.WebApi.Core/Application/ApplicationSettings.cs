namespace LC.RA.WebApi.Core.Application
{
    public sealed class ApplicationSettings : IApplicationSettings
    {
        public string ConnectionString { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string WebApiQueueName { get; set; }

        public string LocationServiceQueueName { get; set; }

        public string WikipediaBaseUrl { get; set; }

        public string WikipediaLocationsPageUrl { get; set; }
    }
}