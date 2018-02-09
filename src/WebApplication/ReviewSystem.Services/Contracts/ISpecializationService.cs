using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}