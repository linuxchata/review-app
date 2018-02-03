using Microsoft.AspNetCore.Mvc;
using Moq;
using ReviewSystem.Controllers;
using ReviewSystem.Services.Contracts;
using Xunit;

namespace ReviewSystem.Tests
{
    public class SynchronizationControllerTests
    {
        private readonly SynchronizationController sut;

        private readonly Mock<ILocationSynchronizationService> synchronizationService;

        public SynchronizationControllerTests()
        {
            this.synchronizationService = new Mock<ILocationSynchronizationService>();

            this.sut = new SynchronizationController(this.synchronizationService.Object);
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
            this.synchronizationService.Verify(a => a.Synchronize());
        }
    }
}