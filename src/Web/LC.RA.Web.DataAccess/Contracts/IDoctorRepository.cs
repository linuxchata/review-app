using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.DataAccess.Contracts
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