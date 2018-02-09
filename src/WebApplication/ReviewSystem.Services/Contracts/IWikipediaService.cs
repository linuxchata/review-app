using System.Threading.Tasks;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}