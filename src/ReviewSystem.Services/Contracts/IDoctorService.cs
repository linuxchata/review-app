using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(string id);

        Task AddAsync(Doctor doctor);

        Task EditAsync(Doctor doctor);

        Task DeleteAsync(string subjectId);
    }
}