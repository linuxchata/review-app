using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }

        public Task<IEnumerable<Subject>> GetAllAsync()
        {
            return this.subjectRepository.GetAllAsync();
        }

        public Task<Subject> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Subject id cannot be null or empty");
            }

            return this.subjectRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");
            }

            return this.subjectRepository.InsertAsync(subject);
        }

        public Task EditAsync(Subject subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject), "Subject cannot be null");
            }

            return this.subjectRepository.UpdateAsync(subject);
        }

        public Task DeleteAsync(string subjectId)
        {
            if (string.IsNullOrEmpty(subjectId))
            {
                throw new ArgumentNullException(nameof(subjectId), "Subject is cannot be null or empty");
            }

            return this.subjectRepository.DeleteAsync(subjectId);
        }
    }
}
