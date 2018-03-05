using System.Collections.Generic;
using LC.RA.Synchronization.Api.Models.Domain;

namespace LC.RA.Synchronization.Api.Infrastructure.Converters
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Location> locations);
    }
}