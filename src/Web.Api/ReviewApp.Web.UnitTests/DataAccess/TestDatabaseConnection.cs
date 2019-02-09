using MongoDB.Driver;

using ReviewApp.Web.DataAccess.Contracts;

namespace ReviewApp.Web.UnitTests.DataAccess
{
    public class TestDatabaseConnection : IDatabaseConnection
    {
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var connectionString = @"mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("reviewdb");
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}