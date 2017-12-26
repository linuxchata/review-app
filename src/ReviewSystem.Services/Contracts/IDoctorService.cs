﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ReviewSystem.Core;

namespace ReviewSystem.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();

        Task<Doctor> GetByIdAsync(string id);

        Task CreateAsync(Doctor doctor);

        Task UpdateAsync(Doctor doctor);

        Task DeleteAsync(string subjectId);
    }
}