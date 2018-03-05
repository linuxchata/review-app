using System;
using System.Diagnostics;

namespace LC.RA.SynchronizationService.Api.Model.Domain
{
    [DebuggerDisplay("{Name} {Region}")]
    [Serializable]
    public sealed class Location
    {
        public Location(string name, string region)
        {
            this.Name = name;
            this.Region = region;
        }

        public string Name { get; set; }

        public string Region { get; set; }

        public string GpsLocation { get; set; }
    }
}