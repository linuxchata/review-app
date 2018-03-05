using System.Collections.Generic;
using LC.RA.SynchronizationService.Api.Model.Domain;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Contracts
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Location> locations);
    }
}