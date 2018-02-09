using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LC.RA.WebApi.Core.Domain;
using LC.RA.WebApi.DataAccess.Contracts;
using LC.RA.WebApi.Services.Contracts;

namespace LC.RA.WebApi.Services
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