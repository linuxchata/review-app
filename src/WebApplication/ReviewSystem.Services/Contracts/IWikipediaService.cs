using System.Threading.Tasks;

namespace ReviewSystem.Services.Contracts
{
    public interface IWikipediaService
    {
        Task<string> GetPageContent();
    }
}