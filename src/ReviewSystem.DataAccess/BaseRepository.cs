using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public abstract class BaseRepository<T>
        where T : CompleteModelBase
    {
        protected readonly IMongoCollection<T> Collection;

        protected BaseRepository(IDatabaseConnection databaseConnection)
        {
            var collectionName = typeof(T).Name.ToLower();
            this.Collection = databaseConnection.GetCollection<T>(collectionName);
        }
    }
}