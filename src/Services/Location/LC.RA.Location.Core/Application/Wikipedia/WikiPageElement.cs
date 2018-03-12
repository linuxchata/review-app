using System.Diagnostics;

namespace LC.RA.Location.Core.Application.Wikipedia
{
    [DebuggerDisplay("{ContentType}: {StartIndex} - {EndIndex}")]
    public sealed class WikiPageElement
    {
        public int StartIndex { get; set; }

        public int EndIndex => this.StartIndex + this.Length;

        public int Length { get; set; }

        public WikiPageContentType ContentType { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Content type is {0}: {1} - {2}", this.ContentType, this.StartIndex, this.EndIndex);
        }
    }
}