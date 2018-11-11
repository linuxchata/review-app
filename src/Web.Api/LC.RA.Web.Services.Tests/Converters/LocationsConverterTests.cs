using System.IO;
using System.Linq;

using Google.Protobuf;

using LC.RA.TransferObjects;

using Microsoft.Extensions.Logging;

using Moq;

using ReviewApp.Web.Services.Converters;

using Xunit;

namespace ReviewApp.Web.Services.Tests.Converters
{
    public class LocationConverterTests
    {
        private readonly LocationsConverter sut;

        public LocationConverterTests()
        {
            var loggerMock = new Mock<ILogger<LocationsConverter>>();
            this.sut = new LocationsConverter(loggerMock.Object);
        }

        [Fact]
        public void Convert_WhenDtoIsNull_ShouldReturnNullResult_Test()
        {
            // Arrange
            // Act
            var result = this.sut.Convert(null);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Any());
        }

        [Fact]
        public void Convert_WhenDtoIsNotNull_ShouldReturnNotNulResultl_Test()
        {
            // Arrange
            var locationsProto = new LocationProto
            {
                Name = "Test",
                Region = "Test",
                Gpslocation = string.Empty
            };
            var locationsArray = this.GetLocationsArray(locationsProto);

            // Act
            var result = this.sut.Convert(locationsArray);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.Equal("Test", result[0].Name);
            Assert.Equal("Test", result[0].Region);
        }

        private byte[] GetLocationsArray(LocationProto locationsProto)
        {
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