using System.Collections.Generic;
using LC.RA.Synchronization.Api.Infrastructure.Extensions;
using LC.RA.Synchronization.Api.Infrastructure.Services;
using LC.RA.Synchronization.Api.Models.Application.Wikipedia;

namespace LC.RA.Synchronization.Api.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}