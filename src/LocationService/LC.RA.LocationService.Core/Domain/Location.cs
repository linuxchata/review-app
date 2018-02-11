using System;
using System.Diagnostics;

namespace LC.RA.LocationService.Core.Domain
{
    [DebuggerDisplay("{Name} {Region}")]
    [Serializable]
    public sealed class Location
    {
        public Location()
        {
        }

        public Location(string name, string region)
        {
            this.Name = name;
            this.Region = region;
        }

        public string Name { get; }

        public string Region { get; }

        public string GpsLocation { get; set; }
    }
}