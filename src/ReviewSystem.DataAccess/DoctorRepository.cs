﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class DoctorRepository : BaseRepository<Doctor, DoctorDto>, IDoctorRepository
    {
        private readonly ILogger logger;

        private readonly IDoctorConverter converter;

        public DoctorRepository(
            IDatabaseConnection databaseConnection,
            IDoctorConverter converter,
            ILogger<DoctorRepository> logger) :
            base(databaseConnection)
        {
            this.logger = logger;
            this.converter = converter;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            this.logger.LogDebug("Receiving all doctors");

            var cursor = await this.Collection.FindAsync(FilterDefinition<DoctorDto>.Empty);
            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("All doctors have been received");

            return result;
        }

        public async Task<Doctor> GetByIdAsync(string id)
        {
            this.logger.LogDebug("Receiving doctor with {id}", id);

            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            var result = this.converter.Convert(cursor.FirstOrDefault());

            this.logger.LogDebug("Doctor with {id} has been received");

            return result;
        }

        public async Task<IEnumerable<Doctor>> GetByNamesAsync(Doctor doctor)
        {
            var filter = new
            {
                FirstName = this.GetLowerCaseString(doctor.FirstName),
                LastName = this.GetLowerCaseString(doctor.LastName),
                MiddleName = this.GetLowerCaseString(doctor.MiddleName)
            };

            this.logger.LogDebug("Receiving doctor by {lastName} {firstName} {middleName}", filter.LastName, filter.FirstName, filter.MiddleName);

            var cursor = await this.Collection.FindAsync(a => a.FirstName.ToLower().Contains(filter.FirstName) &&
                                                              a.LastName.ToLower().Contains(filter.LastName) &&
                                                              a.MiddleName.ToLower().Contains(filter.MiddleName));

            var result = cursor.ToEnumerable().Select(a => this.converter.Convert(a));

            this.logger.LogDebug("Doctors by {lastName} {firstName} {middleName} have been received", filter.LastName, filter.FirstName, filter.MiddleName);

            return result;
        }

        public Task InsertAsync(Doctor entity, string user)
        {
            this.logger.LogDebug("Inserting a new doctor {name} by {user}", entity.Name, user);

            var dateTimeNow = DateTime.Now;
            entity.Created = dateTimeNow;
            entity.Updated = dateTimeNow;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.InsertOneAsync(dto)
                .ContinueWith(_ =>
                {
                    entity.Id = dto.Id;

                    this.logger.LogDebug("A new doctor {name} has been inserted by {user}", entity.Name, user);
                });
        }

        public Task UpdateAsync(Doctor entity, string user)
        {
            this.logger.LogDebug("Updating doctor {name} by {user}", entity.Name, user);

            entity.Updated = DateTime.Now;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.ReplaceOneAsync(a => a.Id == dto.Id, dto)
                .ContinueWith(_ =>
                {
                    this.logger.LogDebug("Doctor {name} has been updated by {user}", entity.Name, user);
                });
        }

        public Task DeleteAsync(string id)
        {
            this.logger.LogDebug("Deleting doctor with {id}", id);

            return this.Collection.DeleteOneAsync(a => a.Id == id)
                .ContinueWith(_ =>
                {
                    this.logger.LogDebug("Doctor with {id} has been deleted", id);
                });
        }

        private string GetLowerCaseString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.ToLower() : string.Empty;
        }
    }
}