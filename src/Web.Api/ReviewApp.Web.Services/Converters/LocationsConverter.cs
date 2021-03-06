﻿using System;
using System.Collections.Generic;
using System.IO;

using AutoMapper;

using Microsoft.Extensions.Logging;

using ReviewApp.TransferObjects;
using ReviewApp.Web.Services.Contracts;

using Location = ReviewApp.Web.Core.Domain.Location;

namespace ReviewApp.Web.Services.Converters
{
    public sealed class LocationsConverter : ILocationsConverter
    {
        private readonly IMapper mapper;

        private readonly ILogger<LocationsConverter> logger;

        public LocationsConverter(ILogger<LocationsConverter> logger)
        {
            this.logger = logger;

            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<LocationProto, Location>();
            });

            this.mapper = new Mapper(config);
        }

        public List<Location> Convert(byte[] locationsMessage)
        {
            var locations = new List<Location>();

            try
            {
                LocationsProto locationsProto;
                using (var stream = new MemoryStream())
                {
                    stream.Write(locationsMessage, 0, locationsMessage.Length);
                    stream.Seek(0, SeekOrigin.Begin);

                    locationsProto = LocationsProto.Parser.ParseFrom(locationsMessage);
                }

                foreach (var protoBufLocation in locationsProto.Locations)
                {
                    var location = this.mapper.Map<Location>(protoBufLocation);
                    locations.Add(location);
                }
            }
            catch (Exception exception)
            {
                this.logger.LogError("Error parsing protobuf data: {Exception}", exception);
            }

            return locations;
        }
    }
}