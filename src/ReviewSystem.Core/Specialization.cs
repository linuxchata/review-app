using System.Diagnostics;

namespace ReviewSystem.Core
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : CompleteModelBase
    {
        public string Name { get; set; }
    }
}