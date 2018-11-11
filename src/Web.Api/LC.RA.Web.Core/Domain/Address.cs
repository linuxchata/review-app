namespace ReviewApp.Web.Core.Domain
{
    public sealed class Address : EmbededModelBase
    {
        public string State { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string FormattedAddress => string.Format("{0} {1}, {2}", this.ZipCode, this.City, this.Street);
    }
}