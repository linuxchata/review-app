using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.DataAccess.Contracts;
using LC.RA.WebApi.Services.Contracts;
using LC.ServiceBusAdapter.Abstractions;
using Moq;
using Xunit;

namespace LC.RA.WebApi.Services.Tests
{
    public class LocationServiceTests
    {
        private readonly ILocationService sut;

        private readonly Mock<ILocationRepository> locationRepositoryMock;

        public LocationServiceTests()
        {
            this.locationRepositoryMock = new Mock<ILocationRepository>();
            var queueMessageSenderServiceMock = new Mock<IQueueMessageSenderService>();
            this.sut = new LocationService(this.locationRepositoryMock.Object, queueMessageSenderServiceMock.Object);
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

        [Fact]
        public async void CreateAsync_WhenSubjectIsNull_ShouldThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => this.sut.CreateAsync(null));
            this.locationRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Location>(), string.Empty), Times.Never);
        }

        [Fact]
        public async void CreateAsync_WhenSubjectIsValid_ShouldNotThrowException_Test()
        {
            // Arrange
            // Act
            // Assert
            await this.sut.CreateAsync(new Location());
            this.locationRepositoryMock.Verify(a => a.InsertAsync(It.IsAny<Location>(), null), Times.Once);
        }
    }
}
