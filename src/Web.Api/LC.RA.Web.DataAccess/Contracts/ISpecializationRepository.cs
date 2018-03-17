using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<Specialization> GetByIdAsync(string id);

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}