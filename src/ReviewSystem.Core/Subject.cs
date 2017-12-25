using System.Collections.Generic;
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

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string CertificateNumber { get; set; }

        public List<string> Universities { get; set; }

        public List<string> Internships { get; set; }

        public List<string> Degrees { get; set; }
        
        public List<string> Specializations { get; set; }

        public List<string> Publications { get; set; }

        public List<string> Diseases { get; set; }

        public List<string> Languages { get; set; }

        public Facility Facility { get; set; }

        public decimal GeneralRaiting { get; set; }
    }
}