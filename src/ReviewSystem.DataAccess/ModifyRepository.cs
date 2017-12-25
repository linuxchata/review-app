using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public sealed class ModifyRepository<T> : ReadRepository<T>, IModifyRepository<T>
        where T : RootModelBase
    {
        public ModifyRepository(IDatabaseConnection databaseConnection) :
            base(databaseConnection)
        {
        }

        public Task InsertAsync(T entity, string user)
        {
            entity.Created = DateTime.Now;
            entity.Updated = DateTime.Now;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            return this.Collection.InsertOneAsync(entity);
        }

        public Task UpdateAsync(T entity, string user)
        {
            entity.Updated = DateTime.Now;
            entity.UpdatedBy = user;

            return this.Collection.ReplaceOneAsync(a => a.Id == entity.Id, entity);
        }

        public Task DeleteAsync(string id)
        {
            return this.Collection.DeleteOneAsync(a => a.Id == id);
        }
    }
}