using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Base
{
    public abstract class BaseDbModel
    {

        [BsonId]
        public string Id { get; set; }
    }
}
