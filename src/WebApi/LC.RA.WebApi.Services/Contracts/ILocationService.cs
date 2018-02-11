using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task CreateAsync(Location location, string user = null);

        Task Synchronize();
    }
}