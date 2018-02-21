using LC.RA.WebApi.Controllers;
using LC.RA.WebApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LC.RA.WebApi.Tests
{
    public class SyncControllerTests
    {
        private readonly SyncController sut;

        private readonly Mock<ILocationService> locationServiceMock;

        public SyncControllerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();

            this.sut = new SyncController(this.locationServiceMock.Object);
        }

        [Fact]
        public void SyncLocations_WhenNoErrors_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            var response = this.sut.SyncLocations();

            // Assert
            var result = Assert.IsType<OkResult>(response);
            Assert.NotNull(result);
            this.locationServiceMock.Verify(a => a.Synchronize());
        }
    }
}