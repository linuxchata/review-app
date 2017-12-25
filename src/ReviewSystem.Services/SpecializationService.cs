using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;
using ReviewSystem.Services.Contracts;

namespace ReviewSystem.Services
{
    public sealed class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository specializationRepository;

        public SpecializationService(ISpecializationRepository specializationRepository)
        {
            this.specializationRepository = specializationRepository;
        }

        public Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return this.specializationRepository.GetAllAsync();
        }

        public Task<IEnumerable<Specialization>> GetBySearchCriteriaAsync(string searchCriteria)
        {
            if (string.IsNullOrEmpty(searchCriteria))
            {
                throw new ArgumentNullException(nameof(searchCriteria), "Search criteria cannot be null or empty");
            }

            return this.specializationRepository.GetBySearchCriteriaAsync(searchCriteria);
        }
    }
}