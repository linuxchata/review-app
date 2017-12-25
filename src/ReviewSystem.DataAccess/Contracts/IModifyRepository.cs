using System.Threading.Tasks;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface IModifyRepository<T> : IReadRepository<T>
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(string id);
    }
}