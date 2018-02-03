using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;
using ReviewSystem.Core.Domain;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.DataAccess.Converters;
using Xunit;

namespace ReviewSystem.DataAccess.Tests
{
    [Trait("Category", "IntegrationTests")]
    public class DoctorRepositoryTests
    {
        private readonly Mock<ILogger<DoctorRepository>> logger;

        private readonly IDoctorConverter converter;

        private readonly Doctor testEntity;

        private IDatabaseConnection databaseConnection;

        public DoctorRepositoryTests()
        {
            this.logger = new Mock<ILogger<DoctorRepository>>();

            this.converter = new DoctorConverter();

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
                    Name = "Clinic in New York",
                    Address = new Address
                    {
                        City = "New York",
                        Region = "MA"
                    }
                },
                GeneralRaiting = 5.0m
            };
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnNotNullResult_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

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
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            var result = sut.GetByIdAsync("5a4143315725fe20903659cb").Result;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetByNamesAsync_WhenNamesAreMatching_ShouldReturnMatchingSubjects_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);
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

        [Fact]
        public async void InsertAsync_WhenEntityIsValid_ShouldInsertEntity_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            await sut.InsertAsync(this.testEntity, "TestUser");

            // Assert
            Assert.NotNull(this.testEntity.Id);
            Assert.NotEqual(DateTime.MinValue, this.testEntity.Created);
            Assert.NotEqual(DateTime.MinValue, this.testEntity.Updated);
            Assert.True(this.testEntity.Updated == this.testEntity.Created);
            Assert.Equal("TestUser", this.testEntity.CreatedBy);
            Assert.Equal("TestUser", this.testEntity.UpdatedBy);
        }

        [Fact]
        public async void UpdateAsync_WhenEntityExists_ShouldUpdateEntity_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);
            await sut.InsertAsync(this.testEntity, "TestUser");
            this.testEntity.FirstName = "Valentino";
            this.testEntity.LastName = "Rossi";

            // Act
            await sut.UpdateAsync(this.testEntity, "TestUserForUpdate");

            // Assert
            var entities = sut.GetAllAsync().Result.ToList();
            var updatedResult = entities.First(a => a.Id == this.testEntity.Id);
            Assert.Equal("Valentino", updatedResult.FirstName);
            Assert.Equal("Rossi", updatedResult.LastName);

            Assert.NotEqual(DateTime.MinValue, this.testEntity.Created);
            Assert.NotEqual(DateTime.MinValue, this.testEntity.Updated);
            Assert.True(this.testEntity.Updated > this.testEntity.Created);
            Assert.Equal("TestUser", this.testEntity.CreatedBy);
            Assert.Equal("TestUserForUpdate", this.testEntity.UpdatedBy);
        }

        [Fact]
        public async void UpdateAsync_WhenEntityDoesNotExist_ShouldNotUpdateEntity_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);

            // Act
            await sut.UpdateAsync(this.testEntity, "TestUserForUpdate");

            // Assert
            var entities = sut.GetAllAsync().Result.ToList();
            var updatedResult = entities.FirstOrDefault(a => a.Id == this.testEntity.Id);
            Assert.Null(updatedResult);
        }

        [Fact]
        public async void DeleteAsync_WhenEntityExists_ShouldDeleteEntity_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);
            await sut.InsertAsync(this.testEntity, string.Empty);

            // Act
            await sut.DeleteAsync(this.testEntity.Id);

            // Assert
            var entities = sut.GetAllAsync().Result.ToList();
            Assert.DoesNotContain(entities, a => string.Equals(a.Id, this.testEntity.Id));
        }

        [Fact]
        public async void DeleteAsync_WhenEntityDoesNotExist_ShouldNotDeleteEntity_Test()
        {
            // Arrange
            var sut = new DoctorRepository(this.GetDatabaseConnection(), this.converter, this.logger.Object);
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