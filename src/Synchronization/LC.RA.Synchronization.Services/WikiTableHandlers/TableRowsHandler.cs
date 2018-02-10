using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LC.RA.Synchronization.Core.Application.Wikipedia;

namespace LC.RA.Synchronization.Services.WikiTableHandlers
{
    public sealed class TableRowsHandler : TableBaseHandler
    {
        protected override void HandlerRequestInternal(ref string content, List<WikiTableRowBase> rows)
        {
            var collections = RegexExtension.GetMatches(content, RegexPattern.TableRowMatchPattern);

            foreach (Match collection in collections)
            {
                var group = collection.Groups[0];

                var row = new WikiTableRow();
                row.Content.AddRange(this.GetRowColumnsContent(group.Value));
                rows.Add(row);
            }
        }

        private List<string> GetRowColumnsContent(string @string)
        {
            // Remove leading row separator (|-)
            RegexExtension.Replace(ref @string, RegexPattern.TableRowSeparatorMatchPattern);

            // Remove ending new line symbol
            @string = @string.Trim();

            // Split by new lines
            var columnsRow = @string.Split('\n');

            return columnsRow.ToList();
        }
    }
}