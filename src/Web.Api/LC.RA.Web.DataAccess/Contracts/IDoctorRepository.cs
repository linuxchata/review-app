using System.Collections.Generic;
using System.Threading.Tasks;

using ReviewApp.Web.Core.Domain;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(string id);

        Task<IEnumerable<Doctor>> GetByNamesAsync(Doctor doctor);

        Task InsertAsync(Doctor entity, string user);

        Task UpdateAsync(Doctor entity, string user);

        Task DeleteAsync(string id);
    }
}