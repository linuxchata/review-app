using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ILocationRepository : IReadRepository<Location>
    {
        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}