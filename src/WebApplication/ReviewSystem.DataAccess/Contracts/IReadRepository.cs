using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);
    }
}