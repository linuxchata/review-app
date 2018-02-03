using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core.Domain;

namespace ReviewSystem.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task CreateAsync(Location location, string user = null);
    }
}