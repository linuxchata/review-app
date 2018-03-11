using System.Collections.Generic;

namespace LC.RA.Location.Infrastructure.Converters
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Core.Domain.Location> locations);
    }
}