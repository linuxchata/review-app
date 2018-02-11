using LC.RA.WebApi.Controllers;
using LC.RA.WebApi.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LC.RA.WebApi.Tests
{
    public class SynchronizationControllerTests
    {
        private readonly SynchronizationController sut;

        private readonly Mock<ILocationService> locationServiceMock;

        public SynchronizationControllerTests()
        {
            this.locationServiceMock = new Mock<ILocationService>();

            this.sut = new SynchronizationController(this.locationServiceMock.Object);
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