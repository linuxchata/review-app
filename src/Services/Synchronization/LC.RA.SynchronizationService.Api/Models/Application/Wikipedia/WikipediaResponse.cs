namespace LC.RA.Synchronization.Api.Models.Application.Wikipedia
{
    public sealed class WikipediaResponse
    {
        public bool Batchcomplete { get; set; }

        public WikipediaQueryResponse Query { get; set; }
    }
}
