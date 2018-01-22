using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core.Domain;
using ReviewSystem.Core.TransferObjects;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class DoctorRepository : BaseRepository<Doctor, DoctorDto>, IDoctorRepository
    {
        private readonly IDoctorConverter converter;

        public DoctorRepository(IDatabaseConnection databaseConnection, IDoctorConverter converter) :
            base(databaseConnection)
        {
            this.converter = converter;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            var cursor = await this.Collection.FindAsync(FilterDefinition<DoctorDto>.Empty);
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }

        public async Task<Doctor> GetByIdAsync(string id)
        {
            var cursor = await this.Collection.FindAsync(a => a.Id == id);
            return this.converter.Convert(cursor.FirstOrDefault());
        }

        public async Task<IEnumerable<Doctor>> GetByNamesAsync(Doctor doctor)
        {
            var fiter = new
            {
                FirstName = this.GetLowerCaseString(doctor.FirstName),
                LastName = this.GetLowerCaseString(doctor.LastName),
                MiddleName = this.GetLowerCaseString(doctor.MiddleName)
            };

            var cursor = await this.Collection.FindAsync(a => a.FirstName.ToLower().Contains(fiter.FirstName) &&
                                                              a.LastName.ToLower().Contains(fiter.LastName) &&
                                                              a.MiddleName.ToLower().Contains(fiter.MiddleName));
            return cursor.ToEnumerable().Select(a => this.converter.Convert(a));
        }

        public Task InsertAsync(Doctor entity, string user)
        {
            var dateTimeNow = DateTime.Now;
            entity.Created = dateTimeNow;
            entity.Updated = dateTimeNow;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.InsertOneAsync(dto)
                .ContinueWith(_ => { entity.Id = dto.Id; });
        }

        public Task UpdateAsync(Doctor entity, string user)
        {
            entity.Updated = DateTime.Now;
            entity.UpdatedBy = user;

            var dto = this.converter.Convert(entity);

            return this.Collection.ReplaceOneAsync(a => a.Id == dto.Id, dto);
        }

        public Task DeleteAsync(string id)
        {
            return this.Collection.DeleteOneAsync(a => a.Id == id);
        }

        private string GetLowerCaseString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.ToLower() : string.Empty;
        }
    }
}