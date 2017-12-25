using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ISpecializationRepository : IReadRepository<Specialization>
    {
        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}