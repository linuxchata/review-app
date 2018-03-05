using System.Collections.Generic;
using System.Diagnostics;

namespace LC.RA.SynchronizationService.Api.Models.Application.Wikipedia
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