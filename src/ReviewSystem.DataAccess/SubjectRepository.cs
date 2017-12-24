using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ReviewSystem.Core;
using ReviewSystem.DataAccess.Contracts;

namespace ReviewSystem.DataAccess
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IMongoCollection<Subject> collection;

        public SubjectRepository(IDatabaseConnection databaseConnection)
        {
            var collectionName = typeof(Subject).Name.ToLower();
            this.collection = databaseConnection.GetCollection<Subject>(collectionName);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            var cursor = await this.collection.FindAsync(FilterDefinition<Subject>.Empty);
            return cursor.ToEnumerable();
        }

        public async Task<Subject> GetByIdAsync(string id)
        {
            var cursor = await this.collection.FindAsync(a => a.Id == id);
            return cursor.FirstOrDefault();
        }

        public Task InsertAsync(Subject subject)
        {
            if (string.IsNullOrEmpty(subject.Id))
            {
                subject.Id = ObjectId.GenerateNewId().ToString();
            }

            return this.collection.InsertOneAsync(subject);
        }

        public Task UpdateAsync(Subject subject)
        {
            return this.collection.ReplaceOneAsync(a => a.Id == subject.Id, subject);
        }

        public Task DeleteAsync(string id)
        {
            return this.collection.DeleteOneAsync(a => a.Id == id);
        }
    }
}