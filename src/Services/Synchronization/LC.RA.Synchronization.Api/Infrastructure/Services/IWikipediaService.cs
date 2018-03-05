using System.Threading.Tasks;

namespace LC.RA.Synchronization.Api.Infrastructure.Services
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}