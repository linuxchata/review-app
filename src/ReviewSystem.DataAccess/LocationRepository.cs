using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMongoCollection<Location> collection;

        public LocationRepository(IDatabaseConnection databaseConnection)
        {
            var collectionName = typeof(Location).Name.ToLower();
            this.collection = databaseConnection.GetCollection<Location>(collectionName);
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var cursor = await this.collection.FindAsync(FilterDefinition<Location>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var criteria = searchCriteria.ToLower();
            var cursor = await this.collection.FindAsync(a => a.Name.ToLower().Contains(criteria));
            return cursor.ToEnumerable();
        }
    }
}
