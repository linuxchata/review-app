using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Core.TransferObjects;
using LC.RA.WebApi.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LC.RA.WebApi.DataAccess
{
    public sealed class SpecializationRepository : BaseRepository<Specialization, SpecializationDto>, ISpecializationRepository
    {
        private readonly ILogger<SpecializationRepository> logger;

        private readonly ISpecializationConverter converter;

        public SpecializationRepository(
            IDatabaseConnection databaseConnection,
            ISpecializationConverter converter,
            ILogger<SpecializationRepository> logger) :
            base(databaseConnection)
        {
            this.logger = logger;
            this.converter = converter;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            this.logger.LogDebug("Receiving all specializations");

            var cursor = await this.Collection.FindAsync(FilterDefinition<SpecializationDto>.Empty);
            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("All specializations have been received");

            return result;
        }

        public async Task<Specialization> GetByIdAsync(string id)
        {
            this.logger.LogDebug("Receiving specialization with {Id}", id);

            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            var result = this.converter.Convert(cursor.FirstOrDefault());

            this.logger.LogDebug("Specialization with {Id} has been received");

            return result;
        }

        public async Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            var filter = searchCriteria.ToLower();

            this.logger.LogDebug("Receiving specializations by {SearchCriteria}", filter);

            var cursor = await this.Collection.FindAsync(a => a.Name.ToLower().Contains(filter));
            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("Specializations by {SearchCriteria} have been received", filter);

            return result;
        }
    }
}