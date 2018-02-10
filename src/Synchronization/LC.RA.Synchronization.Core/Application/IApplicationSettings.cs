namespace LC.RA.Synchronization.Core.Application
{
    public interface IApplicationSettings
    {
        string WikipediaBaseUrl { get; set; }

        string WikipediaLocationsPageUrl { get; set; }
    }
}