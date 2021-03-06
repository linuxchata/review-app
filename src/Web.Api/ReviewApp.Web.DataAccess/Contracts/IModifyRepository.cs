﻿using System.Threading.Tasks;

namespace ReviewApp.Web.DataAccess.Contracts
{
    public interface IModifyRepository<T> : IReadRepository<T>
    {
        Task InsertAsync(T entity, string user);

        Task UpdateAsync(T entity, string user);

        Task DeleteAsync(string id);
    }
}