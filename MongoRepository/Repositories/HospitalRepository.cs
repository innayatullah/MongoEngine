using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using MongoRepository.Base;
using MongoRepository.DbModels;

namespace MongoRepository.Repositories
{
    public class HospitalRepository : BaseRepository<Hospital>
    {
        public HospitalRepository() : base(Utilities.Configurations.ConnectionString)
        {
        }

        public override Hospital Add(Hospital item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            item.LastModified = DateTime.UtcNow;
            Collection.Insert(item);
            return item;
        }

        public override bool Update(Hospital item)
        {
            var query = Query.EQ("_id", item.Id);
            item.LastModified = DateTime.UtcNow;
            IMongoUpdate update = Update<Hospital>
                .Set(h => h.Email, item.Email)
                .Set(h => h.LastModified, DateTime.UtcNow)
                .Set(h => h.Name, item.Name)
                .Set(h => h.Phone, item.Phone);
            Collection.Update(query, update);
            return true;
        }
    }
}
