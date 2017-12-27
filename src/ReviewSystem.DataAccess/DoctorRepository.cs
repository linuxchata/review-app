using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class DoctorRepository : ModifyRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public async Task<IEnumerable<Doctor>> GetByNamesAsync(Doctor doctor)
        {
            var filter = Builders<Doctor>.Filter.And(new[]
            {
                GetFilterDefinition(nameof(doctor.FirstName), doctor.FirstName.ToLower()),
                GetFilterDefinition(nameof(doctor.LastName), doctor.LastName.ToLower()),
                GetFilterDefinition(nameof(doctor.MiddleName), doctor.MiddleName.ToLower())
            });

            var cursor = await this.Collection.FindAsync(filter);
            return cursor.ToEnumerable();
        }

        private FilterDefinition<Doctor> GetFilterDefinition(string field, string value)
        {
            return Builders<Doctor>.Filter.Eq(field, value);
        }
    }
}