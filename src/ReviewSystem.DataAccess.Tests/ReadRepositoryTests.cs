using System.Linq;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class ReadRepositoryTests
    {
        private IDatabaseConnection databaseConnection;

        [Fact]
        public async void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new ReadRepository<Location>(this.GetDatabaseConnection());

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
            var sut = new ReadRepository<Location>(this.GetDatabaseConnection());

            // Act
            var result = sut.GetByIdAsync("5a3edb4f5ac5641b4ca8e650").Result;

            // Assert
            Assert.NotNull(result);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}