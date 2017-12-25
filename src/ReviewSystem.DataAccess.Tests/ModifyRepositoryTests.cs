using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class ModifyRepositoryTests
    {
        private readonly Doctor testEntity;

        private IDatabaseConnection databaseConnection;

        public ModifyRepositoryTests()
        {
            this.testEntity = new Doctor
            {
                FirstName = "John",
                MiddleName = "Richard",
                LastName = "Oliver",
                CertificateNumber = "OR-056-RT",
                Schools = new List<string> { "Wroclaw" },
                Degrees = new List<string> { "Master" },
                Specializations = new List<string> { "Surgeon" },
                Diseases = new List<string> { "Cold" },
                Languages = new List<string> { "English" },
                Facility = new Facility
                {
                    Name = "Clinic in New York"
                },
                GeneralRaiting = 5.0m
            };
        }

        [Fact]
        public async void InsertAsync_WhenEntityIsValid_ShouldInsertEntity_Test()
        {
            // Arrange
            var sut = new ModifyRepository<Doctor>(this.GetDatabaseConnection());

            // Act
            await sut.InsertAsync(this.testEntity);

            // Assert
            Assert.NotNull(this.testEntity.Id);
        }

        [Fact]
        public async void UpdateAsync_WhenEntityExists_ShouldUpdateEntity_Test()
        {
            // Arrange
            var sut = new ModifyRepository<Doctor>(this.GetDatabaseConnection());
            await sut.InsertAsync(this.testEntity);
            this.testEntity.FirstName = "Valentino";
            this.testEntity.LastName = "Rossi";

            // Act
            await sut.UpdateAsync(this.testEntity);

            // Assert
            var enteties = sut.GetAllAsync().Result.ToList();
            var updatedResult = enteties.First(a => a.Id == this.testEntity.Id);
            Assert.Equal("Valentino", updatedResult.FirstName);
            Assert.Equal("Rossi", updatedResult.LastName);
        }

        [Fact]
        public async void UpdateAsync_WhenEntityDoesNotExist_ShouldNotUpdateEntity_Test()
        {
            // Arrange
            var sut = new ModifyRepository<Doctor>(this.GetDatabaseConnection());

            // Act
            await sut.UpdateAsync(this.testEntity);

            // Assert
            var enteties = sut.GetAllAsync().Result.ToList();
            var updatedResult = enteties.FirstOrDefault(a => a.Id == this.testEntity.Id);
            Assert.Null(updatedResult);
        }

        [Fact]
        public async void DeleteAsync_WhenEntityExists_ShouldDeleteEntity_Test()
        {
            // Arrange
            var sut = new ModifyRepository<Doctor>(this.GetDatabaseConnection());
            await sut.InsertAsync(this.testEntity);

            // Act
            await sut.DeleteAsync(this.testEntity.Id);

            // Assert
            var enteties = sut.GetAllAsync().Result.ToList();
            Assert.DoesNotContain(enteties, a => string.Equals(a.Id, this.testEntity.Id));
        }

        [Fact]
        public async void DeleteAsync_WhenEntityDoesNotExist_ShouldNotDeleteEntity_Test()
        {
            // Arrange
            var sut = new ModifyRepository<Doctor>(this.GetDatabaseConnection());
            var id = ObjectId.GenerateNewId().ToString();
            var beforeCount = sut.GetAllAsync().Result.Count();

            // Act
            await sut.DeleteAsync(id);

            // Assert
            var afterCount = sut.GetAllAsync().Result.Count();
            Assert.Equal(beforeCount, afterCount);
        }

        private IDatabaseConnection GetDatabaseConnection() =>
            this.databaseConnection ?? (this.databaseConnection = new TestDatabaseConnection());
    }
}
