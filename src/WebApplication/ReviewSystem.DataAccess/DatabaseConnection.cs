using System.Security.Authentication;
using MongoDB.Driver;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class DatabaseConnection : IDatabaseConnection
    {
        private readonly string connectionString;

        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(this.connectionString));
            settings.SslSettings = new SslSettings
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("reviewdb");
            var collection = database.GetCollection<T>(collectionName);
            return collection;
        }
    }
}