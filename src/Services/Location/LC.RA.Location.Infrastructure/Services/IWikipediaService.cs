using System.Threading.Tasks;

namespace LC.RA.Location.Infrastructure.Services
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}