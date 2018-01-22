using System.Linq;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.DataAccess.Converters;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class SpecializationRepositoryTests
    {
        private readonly ISpecializationConverter converter;

        private IDatabaseConnection databaseConnection;

        public SpecializationRepositoryTests()
        {
            this.converter = new SpecializationConverter();
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter);

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
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter);

            // Act
            var result = sut.GetByIdAsync("5a416e6579fa23151c32c990").Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetBySearchCriteriaAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter);

            // Act
            var result = await sut.GetBySearchCriteriaAsync("Oculist");

            // Assert
            var specializations = result.ToList();
            Assert.NotNull(specializations);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}