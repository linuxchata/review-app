using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReviewSystem.Core
{
    [DebuggerDisplay("Subject: {FirstName} {LastName}")]
    public class Subject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Degree { get; set; }

        public string Specialization { get; set; }
    }
}