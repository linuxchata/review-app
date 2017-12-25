using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class SpecializationRepository : ReadRepository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public async Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var criteria = searchCriteria.ToLower();
            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(criteria));
            return cursor.ToEnumerable();
        }
    }
}