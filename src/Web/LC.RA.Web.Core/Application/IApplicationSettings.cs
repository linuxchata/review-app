namespace LC.RA.WebApi.Core.Application
{
    public interface IApplicationSettings
    {
        string ConnectionString { get; set; }

        string ServiceBusConnectionString { get; set; }

        string ServiceBusTopicName { get; set; }

        string ServiceBusSubscriptionName { get; set; }

        string WikipediaBaseUrl { get; set; }

        string WikipediaLocationsPageUrl { get; set; }
    }
}