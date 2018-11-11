using System.Collections.Generic;
using System.Threading.Tasks;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task CreateAsync(Location location, string user = null);

        Task RequestSynchronization();

        void Synchronize(IEnumerable<Location> sourceLocations);
    }
}