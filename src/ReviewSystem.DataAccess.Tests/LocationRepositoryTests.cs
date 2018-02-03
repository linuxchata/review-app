using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using ReviewSystem.Core.Domain;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.DataAccess.Converters;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class LocationRepositoryTests
    {
        private readonly Mock<ILogger<LocationRepository>> logger;

        private readonly ILocationConverter converter;

        private IDatabaseConnection databaseConnection;

        public LocationRepositoryTests()
        {
            this.logger = new Mock<ILogger<LocationRepository>>();

            this.converter = new LocationConverter();
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new LocationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            var result = await sut.GetAllAsync();

            // Assert
            var locations = result.ToList();
            Assert.NotNull(locations);
        }

        [Fact]
        public void GetByIdAsync_WhenIdIsValid_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new LocationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            var result = sut.GetByIdAsync("5a6650e1df1f001100a90c74").Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetBySearchCriteriaAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new LocationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            var result = await sut.GetBySearchCriteriaAsync("Kiev");

            // Assert
            var locations = result.ToList();
            Assert.NotNull(locations);
        }

        [Fact]
        public async void InsertAsync_WhenEntityIsValid_ShouldInsertEntity_Test()
        {
            // Arrange
            var testEntity = new Location("Milan", "Milan");
            var sut = new LocationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            await sut.InsertAsync(testEntity, "TestUser");

            // Assert
            Assert.NotNull(testEntity.Id);
            Assert.NotEqual(DateTime.MinValue, testEntity.Created);
            Assert.NotEqual(DateTime.MinValue, testEntity.Updated);
            Assert.True(testEntity.Updated == testEntity.Created);
            Assert.Equal("TestUser", testEntity.CreatedBy);
            Assert.Equal("TestUser", testEntity.UpdatedBy);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}