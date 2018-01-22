using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core.Domain;

namespace ReviewSystem.Services.Contracts
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}