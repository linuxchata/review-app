using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class LocationRepository : ReadRepository<Location>, ILocationRepository
    {
        public LocationRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public async Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var criteria = searchCriteria.ToLower();
            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(criteria));
            return cursor.ToEnumerable();
        }
    }
}
