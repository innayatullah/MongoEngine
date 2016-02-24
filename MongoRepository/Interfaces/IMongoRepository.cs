/*
*   AUTHOR:     INNAYATULLAH
*/

using System.Collections.Generic;

namespace MongoEngine.Interfaces
{
    public interface IMongoRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(string id);

        T Add(T item);

        bool Remove(string id);

        bool Update(T item);

        void RemoveAll();
    }
}
