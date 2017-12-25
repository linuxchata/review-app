using System.Linq;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class SpecializationRepositoryTests
    {
        private IDatabaseConnection databaseConnection;

        [Fact]
        public async void GetBySearchCriteriaAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new SpecializationRepository(this.GetDatabaseConnection());

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