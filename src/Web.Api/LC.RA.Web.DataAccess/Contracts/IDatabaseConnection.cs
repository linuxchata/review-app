using MongoDB.Driver;

namespace LC.RA.Web.DataAccess.Contracts
{
    public interface IDatabaseConnection
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}