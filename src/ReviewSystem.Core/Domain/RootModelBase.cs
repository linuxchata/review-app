using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReviewSystem.Core.Domain
{
    public abstract class RootModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}