using MongoDB.Driver;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface IDatabaseConnection
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}