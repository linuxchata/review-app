using System.Collections.Generic;
using LC.RA.SynchronizationService.Api.Models.Domain;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Converters
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Location> locations);
    }
}