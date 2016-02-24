using MongoEngine.Base;

namespace WebApi.DbModels
{
    public class Department : BaseDbModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
