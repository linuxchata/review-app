using System.Threading.Tasks;

namespace LC.RA.Synchronization.Services.Contracts
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}