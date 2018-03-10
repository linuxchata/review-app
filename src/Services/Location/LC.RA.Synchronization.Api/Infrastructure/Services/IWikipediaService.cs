using System.Threading.Tasks;

namespace LC.RA.Location.Api.Infrastructure.Services
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}