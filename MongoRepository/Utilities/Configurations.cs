using System.Configuration;

namespace MongoRepository.Utilities
{
    public static class Configurations
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
    }
}
