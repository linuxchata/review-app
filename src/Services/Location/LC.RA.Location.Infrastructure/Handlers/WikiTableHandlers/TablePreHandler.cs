using System.Collections.Generic;
using LC.RA.Location.Core.Application.Wikipedia;
using LC.RA.Location.Infrastructure.Extensions;
using LC.RA.Location.Infrastructure.Services;

namespace LC.RA.Location.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}