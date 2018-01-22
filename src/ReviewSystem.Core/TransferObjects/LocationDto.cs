namespace ReviewSystem.Core.TransferObjects
{
    public sealed class LocationDto : RootModelBaseDto
    {
        public string Name { get; set; }

        public string Region { get; set; }

        public string GpsLocation { get; set; }
    }
}