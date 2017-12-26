using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Services
{
    public sealed class DoctorService : IDoctorService
    {
        private readonly IModifyRepository<Doctor> modifyRepository;

        public DoctorService(IModifyRepository<Doctor> modifyRepository)
        {
            this.modifyRepository = modifyRepository;
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return this.modifyRepository.GetAllAsync();
        }

        public Task<Doctor> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Subject id cannot be null or empty");
            }

            return this.modifyRepository.GetByIdAsync(id);
        }

        public Task CreateAsync(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
            }

            return this.modifyRepository.InsertAsync(doctor, string.Empty);
        }

        public Task UpdateAsync(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
            }

            return this.modifyRepository.UpdateAsync(doctor, string.Empty);
        }

        public Task DeleteAsync(string subjectId)
        {
            if (string.IsNullOrEmpty(subjectId))
            {
                throw new ArgumentNullException(nameof(subjectId), "Subject id cannot be null or empty");
            }

            return this.modifyRepository.DeleteAsync(subjectId);
        }
    }
}
