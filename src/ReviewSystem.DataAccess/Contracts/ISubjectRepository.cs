using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();

        Task<Subject> GetByIdAsync(string id);

        Task InsertAsync(Subject subject);

        Task UpdateAsync(Subject subject);

        Task DeleteAsync(string id);
    }
}