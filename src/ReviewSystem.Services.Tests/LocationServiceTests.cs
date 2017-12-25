using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;
using Xunit;

namespace ReviewSystem.Services.Tests
{
    public class LocationServiceTests
    {
        private readonly ILocationService sut;

        private readonly Mock<ILocationRepository> locationRepositoryMock;

        public LocationServiceTests()
        {
            this.locationRepositoryMock = new Mock<ILocationRepository>();
            this.sut = new LocationService(this.locationRepositoryMock.Object);
        }

        [Fact]
        public async void GetAll_WhenLocationsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location()
            };
            this.locationRepositoryMock
                .Setup(a => a.GetAllAsync())
                .Returns(Task.FromResult<IEnumerable<Location>>(locations));

            // Act
            var result = await this.sut.GetAllAsync();

            // Assert
            Assert.True(result.Any());
            this.locationRepositoryMock.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetBySearchCriteriaAsync_WhenSearchCriteriaIsNullOrEmpty_ShouldThrowException_Test(string searchCriteria)
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.GetBySearchCriteriaAsync(searchCriteria));
            this.locationRepositoryMock.Verify(a => a.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async void GetBySearchCriteriaAsync_WhenMatchedLocationsExist_ShouldReturnNotEmptyResult_Test()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location()
            };
            this.locationRepositoryMock
                .Setup(a => a.GetBySearchCriteriaAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<IEnumerable<Location>>(locations));

            // Act
            var result = await this.sut.GetBySearchCriteriaAsync("Kiev");

            // Assert
            Assert.True(result.Any());
            this.locationRepositoryMock.Verify(a => a.GetBySearchCriteriaAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
