using MongoDB.Driver;

namespace ReviewSystem.DataAccess.Contracts
{
    public interface IDatabaseConnection
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}