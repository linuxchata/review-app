using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.RA.Web.Core.Domain;
using LC.RA.Web.DataAccess.Contracts;
using LC.RA.Web.Services.Contracts;

namespace LC.RA.Web.Services
{
    public sealed class SubjectService : ISubjectService
    {
        private readonly IDoctorRepository doctorRepository;

        public SubjectService(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return this.doctorRepository.GetAllAsync();
        }

        public Task<Doctor> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Subject id cannot be null or empty");
            }

            return this.doctorRepository.GetByIdAsync(id);
        }

        public Task<bool> ExistsAsync(Doctor subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");
            }

            var task = this.doctorRepository.GetByNamesAsync(subject);

            return task.ContinueWith(t => t.Result.Any());
        }

        public Task CreateAsync(Doctor subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");
            }

            return this.doctorRepository.InsertAsync(subject, string.Empty);
        }

        public Task UpdateAsync(Doctor subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");
            }

            return this.doctorRepository.UpdateAsync(subject, string.Empty);
        }

        public Task DeleteAsync(string subjectId)
        {
            if (string.IsNullOrEmpty(subjectId))
            {
                throw new ArgumentNullException(nameof(subjectId), "Subject id cannot be null or empty");
            }

            return this.doctorRepository.DeleteAsync(subjectId);
        }
    }
}
