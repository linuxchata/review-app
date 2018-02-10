namespace LC.RA.LocationService.Core.Application
{
    public sealed class ApplicationSettings : IApplicationSettings
    {
        public string WikipediaBaseUrl { get; set; }

        public string WikipediaLocationsPageUrl { get; set; }
    }
}