using System.Diagnostics;

namespace LC.RA.SynchronizationService.Api.Models.Application.Wikipedia
{
    [DebuggerDisplay("{ContentType}: {StartIndex} - {EndIndex}")]
    public sealed class WikiPageElement
    {
        public int StartIndex { get; set; }

        public int EndIndex => this.StartIndex + this.Length;

        public int Length { get; set; }

        public WikiPageContentType ContentType { get; set; }

        public string Content { get; set; }
    }
}