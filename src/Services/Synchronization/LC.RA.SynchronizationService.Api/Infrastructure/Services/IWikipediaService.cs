using System.Threading.Tasks;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Services
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}