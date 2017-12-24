using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.Services.Contracts
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllAsync();

        Task<Subject> GetByIdAsync(string id);

        Task AddAsync(Subject subject);

        Task EditAsync(Subject subject);

        Task DeleteAsync(string subjectId);
    }
}