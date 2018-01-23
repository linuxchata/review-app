namespace ReviewSystem.Core.Application
{
    public sealed class ApplicationSettings : IApplicationSettings
    {
        public string WikipediaBaseUrl { get; set; }

        public string WikipediaLocationsPageUrl { get; set; }
    }
}