using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReviewSystem.Core
{
    [DebuggerDisplay(nameof(Name))]
    public sealed class Facility
    {
        public Facility()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string GpsLocation { get; set; }

        public Address Address { get; set; }
    }
}