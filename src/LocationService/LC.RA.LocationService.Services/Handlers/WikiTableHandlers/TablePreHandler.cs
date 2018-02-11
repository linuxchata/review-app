using System.Collections.Generic;
using LC.RA.LocationService.Core.Application.Wikipedia;
using LC.RA.LocationService.Services.Extension;

namespace LC.RA.LocationService.Services.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}