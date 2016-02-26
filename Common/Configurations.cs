using System.Configuration;

namespace Common
{
    public static class Configurations
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static string EmailAccount => ConfigurationManager.AppSettings["emailService:Account"];
        public static string EmailPassword => ConfigurationManager.AppSettings["emailService:Password"];
    }
}
