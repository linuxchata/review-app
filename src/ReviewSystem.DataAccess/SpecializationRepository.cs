using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class SpecializationRepository : BaseRepository<Specialization, SpecializationDto>, ISpecializationRepository
    {
        private readonly ISpecializationConverter converter;

        public SpecializationRepository(IDatabaseConnection databaseConnection, ISpecializationConverter converter) :
            base(databaseConnection)
        {
            this.converter = converter;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            var cursor = await this.Collection.FindAsync(FilterDefinition<SpecializationDto>.Empty);
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }

        public async Task<Specialization> GetByIdAsync(string id)
        {
            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            return this.converter.Convert(cursor.FirstOrDefault());
        }

        public async Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var criteria = searchCriteria.ToLower();
            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(criteria));
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }
    }
}