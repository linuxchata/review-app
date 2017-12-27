﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.Services.Contracts
{
    public interface ISubjectService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(string id);

        Task CreateAsync(Doctor subject);

        Task UpdateAsync(Doctor subject);

        Task DeleteAsync(string subjectId);
    }
}