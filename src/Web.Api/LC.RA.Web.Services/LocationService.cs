using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.DataAccess.Contracts;
using LC.RA.Web.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;

namespace LC.RA.Web.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly ILocationRepository locationRepository;

        private readonly ITopicSenderService topicSenderService;

        public LocationService(
            ILocationRepository locationRepository,
            ITopicSenderService topicSenderService)
        {
            this.locationRepository = locationRepository;
            this.topicSenderService = topicSenderService;
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

        public async Task RequestSynchronization()
        {
            await this.topicSenderService.SendMessageAsync(BitConverter.GetBytes(true), "LocationApi", "WebApi");
        }

        public async void Synchronize(IEnumerable<Location> sourceLocations)
        {
            var existedLocations = await this.GetExistedLocations();

            foreach (var location in sourceLocations)
            {
                if (!existedLocations.Contains(location))
                {
                    await this.CreateAsync(location, "Synchronization User");
                }
            }
        }

        private async Task<HashSet<Location>> GetExistedLocations()
        {
            return new HashSet<Location>(await this.GetAllAsync());
        }
    }
}