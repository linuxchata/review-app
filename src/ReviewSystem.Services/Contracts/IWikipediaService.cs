using System.Threading.Tasks;
using ReviewSystem.Core.Application.Wikipedia;

namespace ReviewSystem.Services.Contracts
{
    public interface IWikipediaService
    {
        Task<WikipediaResponse> GetPageContent();
    }
}