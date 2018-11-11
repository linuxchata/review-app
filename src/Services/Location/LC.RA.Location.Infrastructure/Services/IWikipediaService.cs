using System.Threading.Tasks;

namespace ReviewApp.Location.Infrastructure.Services
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}