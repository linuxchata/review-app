using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}