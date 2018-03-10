using System.Collections.Generic;
using LC.RA.Location.Api.Infrastructure.Extensions;
using LC.RA.Location.Api.Infrastructure.Services;
using LC.RA.Location.Api.Models.Application.Wikipedia;

namespace LC.RA.Location.Api.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}