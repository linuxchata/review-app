using System.Linq;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class DoctorRepositoryTests
    {
        private IDatabaseConnection databaseConnection;

        [Fact]
        public async void GetByNamesAsync_WhenNamesAreMatching_ShouldReturnMatchingSubjects_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection());
            var subject = new Doctor
            {
                FirstName = "John",
                LastName = "Oliver",
                MiddleName = "Richard"
            };

            // Act
            var result = await sut.GetByNamesAsync(subject);

            // Assert
            var subjects = result.ToList();
            Assert.NotNull(subjects);
            Assert.True(subjects.Any());
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}