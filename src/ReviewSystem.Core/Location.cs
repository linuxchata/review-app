using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay("{Name} {Region}")]
    public sealed class Location : RootModelBase
    {
        public string Name { get; set; }

        public string Region { get; set; }

        public string GpsLocation { get; set; }
    }
}