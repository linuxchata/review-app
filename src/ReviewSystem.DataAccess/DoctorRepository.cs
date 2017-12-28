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
            var fiter = new
            {
                FirstName = this.GetLowerCaseString(doctor.FirstName),
                LastName = this.GetLowerCaseString(doctor.LastName),
                MiddleName = this.GetLowerCaseString(doctor.MiddleName)
            };

            var cursor = await this.Collection.FindAsync(a => a.FirstName.ToLower().Contains(fiter.FirstName) &&
                                                              a.LastName.ToLower().Contains(fiter.LastName) &&
                                                              a.MiddleName.ToLower().Contains(fiter.MiddleName));
            return cursor.ToEnumerable();
        }

        private string GetLowerCaseString(string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value.ToLower() : string.Empty;
        }
    }
}