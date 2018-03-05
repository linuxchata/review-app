using System.Collections.Generic;
using LC.RA.TransferObjects;

namespace LC.RA.LocationService.Services.Contracts
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Location> locations);
    }
}