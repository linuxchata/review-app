using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Location : RootModelBase
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string Region { get; set; }

        public string GpsLocation { get; set; }
    }
}