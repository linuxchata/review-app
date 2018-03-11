using System.Collections.Generic;
using System.Diagnostics;

namespace LC.RA.Location.Core.Application.Wikipedia
{
    [DebuggerDisplay("{Content}")]
    public abstract class WikiTableRowBase
    {
        protected WikiTableRowBase()
        {
            this.Content = new List<string>();
        }

        public List<string> Content { get; set; }
    }
}