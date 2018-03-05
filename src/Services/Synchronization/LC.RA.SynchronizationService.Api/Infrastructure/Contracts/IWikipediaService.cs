using System.Threading.Tasks;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Contracts
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}