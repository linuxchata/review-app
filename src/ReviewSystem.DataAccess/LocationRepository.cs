using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class LocationRepository : BaseRepository<Location, LocationDto>, ILocationRepository
    {
        private readonly ILocationConverter converter;

        public LocationRepository(IDatabaseConnection databaseConnection, ILocationConverter converter) :
            base(databaseConnection)
        {
            this.converter = converter;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var cursor = await this.Collection.FindAsync(FilterDefinition<LocationDto>.Empty);
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }

        public async Task<Location> GetByIdAsync(string id)
        {
            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            return this.converter.Convert(cursor.FirstOrDefault());
        }

        public async Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var criteria = searchCriteria.ToLower();
            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(criteria));
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }

        public Task InsertAsync(Location entity, string user)
        {
            var dateTimeNow = DateTime.Now;
            entity.Created = dateTimeNow;
            entity.Updated = dateTimeNow;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.InsertOneAsync(dto)
                .ContinueWith(_ => { entity.Id = dto.Id; });
        }
    }
}
