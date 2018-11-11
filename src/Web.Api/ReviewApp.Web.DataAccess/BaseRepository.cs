using MongoDB.Driver;

using ReviewApp.Web.DataAccess.Contracts;

namespace ReviewApp.Web.DataAccess
{
    public abstract class BaseRepository<T, TD>
    {
        protected readonly IMongoCollection<TD> Collection;

        protected BaseRepository(IDatabaseConnection databaseConnection)
        {
            var collectionName = typeof(T).Name.ToLower();
            this.Collection = databaseConnection.GetCollection<TD>(collectionName);
        }
    }
}