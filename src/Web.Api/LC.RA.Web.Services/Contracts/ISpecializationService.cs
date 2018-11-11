using System.Collections.Generic;
using System.Threading.Tasks;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.Services.Contracts
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync();

        Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria);
    }
}