using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Moq;

using ReviewApp.ServiceBusAdapter.Abstractions;
using ReviewApp.Web.Core.Domain;
using ReviewApp.Web.DataAccess.Contracts;
using ReviewApp.Web.Services.Contracts;

using Xunit;

namespace ReviewApp.Web.Services.Tests
{
    public class LocationServiceTests
    {
        private readonly ILocationService sut;

        private readonly Mock<ILocationRepository> locationRepositoryMock;

        public LocationServiceTests()
        {
            this.locationRepositoryMock = new Mock<ILocationRepository>();
            var topicSenderServiceMock = new Mock<ITopicSenderService>();
            this.sut = new LocationService(this.locationRepositoryMock.Object, topicSenderServiceMock.Object);
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

        [Fact]
        public void Synchronize_WhenContentIsNotEmpty_ShouldNotThrowException_Test()
        {
            // Arrange
            IEnumerable<Location> locations = new List<Location>
            {
                new Location("Kiev", "Kiev")
            };

            // Act
            // Assert
            this.sut.Synchronize(locations);
        }
    }
}
