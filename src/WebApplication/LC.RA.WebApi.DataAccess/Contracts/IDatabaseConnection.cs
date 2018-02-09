using MongoDB.Driver;

namespace LC.RA.WebApi.DataAccess.Contracts
{
    public interface IDatabaseConnection
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}