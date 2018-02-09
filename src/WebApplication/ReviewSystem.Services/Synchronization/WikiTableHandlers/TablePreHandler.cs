using System.Collections.Generic;
using LC.RA.WebApi.Core.Application.Wikipedia;

namespace LC.RA.WebApi.Services.Synchronization.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}