using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core.Domain;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<Location> GetByIdAsync(string id);

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);

        Task InsertAsync(Location entity, string user);
    }
}