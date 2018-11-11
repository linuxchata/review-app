using System.Collections.Generic;
using System.Threading.Tasks;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<Specialization> GetByIdAsync(string id);

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}