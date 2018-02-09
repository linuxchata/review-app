using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;

namespace LC.RA.WebApi.Services.Contracts
{
    public interface ISubjectService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(string id);

        Task<bool> ExistsAsync(Doctor subject);

        Task CreateAsync(Doctor subject);

        Task UpdateAsync(Doctor subject);

        Task DeleteAsync(string subjectId);
    }
}