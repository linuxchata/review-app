using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.Core.TransferObjects;
using LC.RA.Web.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LC.RA.Web.DataAccess
{
    public sealed class LocationRepository : BaseRepository<Location, LocationDto>, ILocationRepository
    {
        private readonly ILogger logger;

        private readonly ILocationConverter converter;

        public LocationRepository(
            IDatabaseConnection databaseConnection,
            ILocationConverter converter,
            ILogger<LocationRepository> logger) :
            base(databaseConnection)
        {
            this.logger = logger;
            this.converter = converter;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            this.logger.LogDebug("Receiving all locations");

            var cursor = await this.Collection.FindAsync(FilterDefinition<LocationDto>.Empty);
            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("All locations have been received");

            return result;
        }

        public async Task<Location> GetByIdAsync(string id)
        {
            this.logger.LogDebug("Receiving location with {Id}", id);

            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            var result = this.converter.Convert(cursor.FirstOrDefault());

            this.logger.LogDebug("Location with {Id} has been received");

            return result;
        }

        public async Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var filter = searchCriteria.ToLower();

            this.logger.LogDebug("Receiving locations by {SearchCriteria}", filter);

            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(filter));
            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("Locations by {SearchCriteria} have been received", filter);

            return result;
        }

        public Task InsertAsync(Location entity, string user)
        {
            this.logger.LogDebug("Inserting a new location {Name} by {User}", entity.Name, user);

            var dateTimeNow = DateTime.Now;
            entity.Created = dateTimeNow;
            entity.Updated = dateTimeNow;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.InsertOneAsync(dto)
                .ContinueWith(_ =>
                {
                    entity.Id = dto.Id;

                    this.logger.LogDebug("A new location {Name} has been inserted by {User}", entity.Name, user);
                });
        }
    }
}
