namespace LC.RA.LocationService.Core.Application
{
    public interface IApplicationSettings
    {
        string ConnectionString { get; set; }

        string ServiceBusConnectionString { get; set; }

        string WebApiQueueName { get; set; }

        string LocationServiceQueueName { get; set; }

        string WikipediaBaseUrl { get; set; }

        string WikipediaLocationsPageUrl { get; set; }
    }
}