﻿using System.Linq;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class LocationRepositoryTests
    {
        private IDatabaseConnection databaseConnection;

        [Fact]
        public async void GetBySearchCriteriaAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new LocationRepository(this.GetDatabaseConnection());

            // Act
            var result = await sut.GetBySearchCriteriaAsync("Kiev");

            // Assert
            var locations = result.ToList();
            Assert.NotNull(locations);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}