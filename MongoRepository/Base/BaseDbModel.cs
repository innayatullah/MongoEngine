using MongoDB.Bson.Serialization.Attributes;

namespace MongoEngine.Base
{
    public abstract class BaseDbModel
    {

        [BsonId]
        public string Id { get; set; }
    }
}
