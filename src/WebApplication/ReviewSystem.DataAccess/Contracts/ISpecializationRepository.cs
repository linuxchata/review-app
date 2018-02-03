using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core.Domain;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<Specialization> GetByIdAsync(string id);

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}