using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<Location> GetByIdAsync(string id);

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task InsertAsync(Location entity, string user);
    }
}