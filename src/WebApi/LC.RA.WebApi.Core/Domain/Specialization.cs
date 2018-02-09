using System.Diagnostics;

namespace LC.RA.WebApi.Core.Domain
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : RootModelBase
    {
        public string Name { get; set; }
    }
}