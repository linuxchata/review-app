﻿using System.Diagnostics;

namespace ReviewSystem.Core.Domain
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Specialization : RootModelBase
    {
        public string Name { get; set; }
    }
}