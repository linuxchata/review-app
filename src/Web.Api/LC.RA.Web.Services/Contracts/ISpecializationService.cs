using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;

namespace LC.RA.Web.Services.Contracts
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}