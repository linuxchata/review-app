using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public class ReadRepository<T> : BaseRepository<T>, IReadRepository<T>
        where T : CompleteModelBase
    {
        public ReadRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var cursor = await this.Collection.FindAsync(FilterDefinition<T>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            return cursor.FirstOrDefault();
        }
    }
}