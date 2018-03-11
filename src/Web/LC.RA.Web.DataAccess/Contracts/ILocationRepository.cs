using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.DataAccess.Contracts
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<Location> GetByIdAsync(string id);

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task InsertAsync(Location entity, string user);
    }
}