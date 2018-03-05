using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Protobuf;
using LC.RA.SynchronizationService.Api.Infrastructure.Contracts;
using LC.RA.SynchronizationService.Api.Model.Domain;
using LC.RA.TransferObjects;

namespace LC.RA.SynchronizationService.Api.Infrastructure.Converters
{
    public sealed class LocationsConverter : ILocationsConverter
    {
        public byte[] Convert(IEnumerable<Location> locations)
        {
            if (locations == null)
            {
                return new byte[0];
            }

            var locationsProto = locations.Select(a =>
                new LocationProto
                {
                    Name = a.Name ?? string.Empty,
                    Region = a.Region ?? string.Empty,
                    Gpslocation = a.GpsLocation ?? string.Empty
                });

            var protoBufLocations = new LocationsProto();
            protoBufLocations.Locations.Add(locationsProto);

            byte[] locationsArray;
            using (var stream = new MemoryStream())
            {
                protoBufLocations.WriteTo(stream);
                locationsArray = stream.ToArray();
            }

            return locationsArray;
        }
    }
}