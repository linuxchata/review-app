using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Facility : EmbededModelBase
    {
        public string Name { get; set; }

        public string GpsLocation { get; set; }

        public Address Address { get; set; }
    }
}