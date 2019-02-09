using System.Linq;

using Microsoft.Extensions.Logging;

using Moq;

using ReviewApp.Web.DataAccess;
using ReviewApp.Web.DataAccess.Contracts;
using ReviewApp.Web.DataAccess.Converters;

using Xunit;

namespace ReviewApp.Web.UnitTests.DataAccess
{
    [Trait("Category", "IntegrationTests")]
    public class SpecializationRepositoryTests
    {
        private readonly Mock<ILogger<SpecializationRepository>> logger;

        private readonly ISpecializationConverter converter;

        private IDatabaseConnection databaseConnection;

        public SpecializationRepositoryTests()
        {
            this.logger = new Mock<ILogger<SpecializationRepository>>();
            this.converter = new SpecializationConverter();
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

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
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            var result = sut.GetByIdAsync("5a416e6579fa23151c32c990").Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetBySearchCriteriaAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SpecializationRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

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