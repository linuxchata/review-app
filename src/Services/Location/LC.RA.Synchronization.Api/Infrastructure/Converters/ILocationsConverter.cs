using System.Collections.Generic;

namespace LC.RA.Location.Api.Infrastructure.Converters
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Models.Domain.Location> locations);
    }
}