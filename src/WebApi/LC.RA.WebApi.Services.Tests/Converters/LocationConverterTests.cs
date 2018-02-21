using LC.RA.WebApi.Services.Converters;
using Xunit;

namespace LC.RA.WebApi.Services.Tests.Converters
{
    public class LocationConverterTests
    {
        private readonly LocationConverter sut;

        public LocationConverterTests()
        {
            this.sut = new LocationConverter();
        }

        [Fact]
        public void Convert_WhenDtoIsNull_ShouldReturnNullResult_Test()
        {
            // Arrange
            // Act
            var result = this.sut.Convert(null);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Convert_WhenDtoIsNotNull_ShouldReturnNotNulResultl_Test()
        {
            // Arrange
            // Act
            var result = this.sut.Convert(new TransferObjects.Location("Test", "Test"));

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Test", result.Region);
        }
    }
}