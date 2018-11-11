using System.Diagnostics;

namespace ReviewApp.Web.Core.Domain
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : RootModelBase
    {
        public string Name { get; set; }

        public string Specialty { get; set; }
    }
}