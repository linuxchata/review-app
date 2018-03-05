using System.Collections.Generic;
using LC.RA.SynchronizationService.Api.Infrastructure.Extensions;
using LC.RA.SynchronizationService.Api.Infrastructure.Services;
using LC.RA.SynchronizationService.Api.Models.Application.Wikipedia;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Handlers.WikiTableHandlers
{
    public sealed class TablePreHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> columns)
        {
            RegexExtension.Replace(ref content, RegexPattern.TableRowSpanMatchPattern);
        }
    }
}