using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReviewSystem.Core
{
    public sealed class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}