using System.Collections.Generic;
using ReviewSystem.Core.Application.Wikipedia;

namespace ReviewSystem.Services.Synchronization.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}