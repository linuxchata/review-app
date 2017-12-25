using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : RootModelBase
    {
        public string Name { get; set; }
    }
}