using System;
using Common;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoEngine.Base;
using MongoEngine.Utilities;

namespace MongoEngine.Repositories
{
    public class GenericMongoRepository<T> : BaseMongoRepository<T>
    {
        public GenericMongoRepository() : base(Configurations.ConnectionString)
        {
        }

        public override T Add(T item)
        {
            RepositoryHelper.SetValue(item, "Id", ObjectId.GenerateNewId().ToString());
            RepositoryHelper.SetValue(item, "LastModified", DateTime.UtcNow);
            Collection.Insert(item);
            return item;
        }

        public override bool Update(T item)
        {
            try
            {
                var query = Query.EQ("_id", RepositoryHelper.GetValue(item, "Id").ToString());
                RepositoryHelper.SetValue(item, "LastModified", DateTime.UtcNow);

                var update = RepositoryHelper.GetMongoUpdate(item);
                Collection.Update(query, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
