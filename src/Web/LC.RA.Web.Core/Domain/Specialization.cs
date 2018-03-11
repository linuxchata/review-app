using System.Diagnostics;

namespace LC.RA.Web.Core.Domain
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : RootModelBase
    {
        public string Name { get; set; }
    }
}