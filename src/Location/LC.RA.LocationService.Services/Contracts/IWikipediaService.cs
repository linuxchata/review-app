using System.Threading.Tasks;

namespace LC.RA.LocationService.Services.Contracts
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}