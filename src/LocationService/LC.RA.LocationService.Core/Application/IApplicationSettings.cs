namespace LC.RA.LocationService.Core.Application
{
    public interface IApplicationSettings
    {
        string WikipediaBaseUrl { get; set; }

        string WikipediaLocationsPageUrl { get; set; }
    }
}