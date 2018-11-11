namespace ReviewApp.Web.Core.Application
{
    public sealed class ApplicationSettings : IApplicationSettings
    {
        public string ConnectionString { get; set; }

        public string ServiceBusConnectionString { get; set; }

        public string ServiceBusTopicName { get; set; }

        public string ServiceBusSubscriptionName { get; set; }

        public string WikipediaBaseUrl { get; set; }

        public string WikipediaLocationsPageUrl { get; set; }
    }
}