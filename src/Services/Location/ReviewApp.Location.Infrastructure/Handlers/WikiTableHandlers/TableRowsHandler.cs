﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using ReviewApp.Location.Core.Application.Wikipedia;
using ReviewApp.Location.Infrastructure.Extensions;
using ReviewApp.Location.Infrastructure.Services;

namespace ReviewApp.Location.Infrastructure.Handlers.WikiTableHandlers
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
                row.Content.AddRange(GetRowColumnsContent(group.Value));
                rows.Add(row);
            }
        }

        private static List<string> GetRowColumnsContent(string @string)
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