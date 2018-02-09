namespace LC.RA.WebApi.Core.Domain
{
    public sealed class Address : EmbededModelBase
    {
        public string State { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }
}