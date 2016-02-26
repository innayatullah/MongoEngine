using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoEngine.Interfaces;

namespace MongoEngine.Base
{
    public abstract class BaseMongoRepository<T>: IMongoRepository<T>
    {
        protected MongoClient Client;
        protected MongoServer Server;
        protected MongoDatabase Database;
        protected MongoCollection<T> Collection;
        protected string ConnectionString;

        protected BaseMongoRepository(string conn)
        {
            var url = new MongoUrl(conn);
            var client = new MongoClient(url);
#pragma warning disable 618
            var server = client.GetServer();
#pragma warning restore 618
            Database = server.GetDatabase(url.DatabaseName); // WriteConcern defaulted to Acknowledged
            Collection = Database.GetCollection<T>(typeof(T).Name);
        }
        
        public bool Remove(string id)
        {
            var query = Query.EQ("_id", id);
            var result = Collection.Remove(query);
            return result.DocumentsAffected == 1;
        }

        public void RemoveAll()
        {
            var collection = Database.GetCollection<BsonDocument>(typeof(T).Name);
            collection.Drop();
        }

        public IEnumerable<T> GetAll()
        {
            return Collection.FindAll();
        }

        public T Get(string id)
        {
            var query = Query.EQ("_id", id);
            return Collection.Find(query).FirstOrDefault();
        }

        public abstract T Add(T item);

        public abstract bool Update(T item);
    }
}
