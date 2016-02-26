using System.Configuration;

namespace MongoEngine.Utilities
{
    public static class Configurations
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }
}
