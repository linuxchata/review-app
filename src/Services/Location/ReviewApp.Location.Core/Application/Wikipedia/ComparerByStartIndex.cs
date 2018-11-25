using System;
using System.Collections.Generic;

namespace ReviewApp.Location.Core.Application.Wikipedia
{
    public sealed class ComparerByStartIndex : IComparer<WikiPageElement>
    {
        public int Compare(WikiPageElement x, WikiPageElement y)
        {
            if (x == null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y == null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            if (x.StartIndex > y.StartIndex)
            {
                return 1;
            }
            else if (x.StartIndex == y.StartIndex)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}