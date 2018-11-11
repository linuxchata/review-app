using System.Collections.Generic;

namespace ReviewApp.Location.Infrastructure.Converters
{
    public interface ILocationsConverter
    {
        byte[] Convert(IEnumerable<Core.Domain.Location> locations);
    }
}