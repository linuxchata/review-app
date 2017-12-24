using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();

        Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}