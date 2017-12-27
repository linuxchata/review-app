using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface IDoctorRepository : IModifyRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetByNamesAsync(Doctor doctor);
    }
}