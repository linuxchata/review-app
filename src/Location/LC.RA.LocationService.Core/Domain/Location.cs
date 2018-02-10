using System;
using System.Diagnostics;

namespace LC.RA.LocationService.Core.Domain
{
    [DebuggerDisplay("{Name} {Region}")]
    public sealed class Location : IEquatable<Location>
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

        public bool Equals(Location other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(this.Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            var location = obj as Location;
            if (location == null)
            {
                return false;
            }

            return this.Equals(location);
        }

        public override int GetHashCode()
        {
            var hashCode = this.Name != null ? this.Name.GetHashCode() : 0;
            return hashCode;
        }
    }
}