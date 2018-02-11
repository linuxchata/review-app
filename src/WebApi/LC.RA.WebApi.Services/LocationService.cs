using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.DataAccess.Contracts;
using LC.RA.WebApi.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;

namespace LC.RA.WebApi.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        private readonly IQueueMessageSenderService queueService;

        public LocationService(
            ILocationRepository locationRepository,
            IQueueMessageSenderService queueService)
        {
            this.locationRepository = locationRepository;
            this.queueService = queueService;
        }

        public Task<IEnumerable<Location>> GetAllAsync()
        {
            return this.locationRepository.GetAllAsync();
        }

        public Task<IEnumerable<Location>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                throw new ArgumentNullException(nameof(searchCriteria), "Search criteria cannot be null or empty");
            }

            return this.locationRepository.GetBySearchCriteriaAsync(searchCriteria);
        }

        public Task CreateAsync(Location location, string user = null)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location), "Location cannot be null");
            }

            return this.locationRepository.InsertAsync(location, user);
        }

        public async Task Synchronize()
        {
            await this.queueService.SendMessage(BitConverter.GetBytes(true));
        }
    }
}