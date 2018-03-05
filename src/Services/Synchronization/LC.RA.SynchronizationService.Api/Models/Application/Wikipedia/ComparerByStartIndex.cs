using System.Collections.Generic;

namespace LC.RA.Synchronization.Api.Models.Application.Wikipedia
{
    public sealed class ComparerByStartIndex : IComparer<WikiPageElement>
    {
        public int Compare(WikiPageElement x, WikiPageElement y)
        {
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