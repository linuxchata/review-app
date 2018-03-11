using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LC.RA.Web.Core.Domain
{
    public abstract class EmbededModelBase
    {
        protected EmbededModelBase()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}