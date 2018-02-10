using System.Collections.Generic;
using LC.RA.Synchronization.Core.Application.Wikipedia;

namespace LC.RA.Synchronization.Services.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}