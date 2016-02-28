using System.Configuration;

namespace Common
{
    public static class Configurations
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static string EmailAccount => ConfigurationManager.AppSettings["emailService:Account"];

        public static string EmailPassword => ConfigurationManager.AppSettings["emailService:Password"];

        public static string AudianceId => ConfigurationManager.AppSettings["as:AudienceId"];

        public static string AudianceSecret => ConfigurationManager.AppSettings["as:AudienceSecret"];

        public static string Issuer => ConfigurationManager.AppSettings["issuer"];

        public static string ApplicationDomain => ConfigurationManager.AppSettings["applicationDomain"];
    }
}
