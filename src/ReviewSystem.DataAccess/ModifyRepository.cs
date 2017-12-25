﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class ModifyRepository<T> : ReadRepository<T>, IModifyRepository<T>
        where T : CompleteModelBase
    {
        public ModifyRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public Task InsertAsync(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }

            return this.Collection.InsertOneAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            return this.Collection.ReplaceOneAsync(a => a.Id == entity.Id, entity);
        }

        public Task DeleteAsync(string id)
        {
            return this.Collection.DeleteOneAsync(a => a.Id == id);
        }
    }
}