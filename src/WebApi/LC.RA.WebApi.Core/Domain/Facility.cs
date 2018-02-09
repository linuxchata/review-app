using System.Diagnostics;

namespace LC.RA.WebApi.Core.Domain
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Facility : EmbededModelBase
    {
        public string Name { get; set; }

        public string GpsLocation { get; set; }

        public Address Address { get; set; }
    }
}