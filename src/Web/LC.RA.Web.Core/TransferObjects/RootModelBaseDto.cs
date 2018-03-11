using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LC.RA.Web.Core.TransferObjects
{
    public class RootModelBaseDto
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