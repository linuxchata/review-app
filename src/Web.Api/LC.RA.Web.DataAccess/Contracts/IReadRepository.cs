using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);
    }
}