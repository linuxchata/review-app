using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.Services.Contracts;

namespace LC.RA.WebApi.Services
{
    public sealed class LocationSynchronizationService : ILocationSynchronizationService
    {
        private readonly ILocationService locationService;

        public LocationSynchronizationService(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public async void Synchronize(IEnumerable<Location> sourceLocations)
        {
            var existedLocations = await this.GetExistedLocations();

            foreach (var location in sourceLocations)
            {
                if (!existedLocations.Contains(location))
                {
                    await this.locationService.CreateAsync(location, "Synchronization User");
                }
            }
        }
        
        private async Task<HashSet<Location>> GetExistedLocations()
        {
            return new HashSet<Location>(await this.locationService.GetAllAsync());
        }
    }
}