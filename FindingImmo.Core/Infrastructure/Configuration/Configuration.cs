using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FindingImmo.Core.Infrastructure
{
    public static class Configuration
    {
        public static string ConnectionString { get; }

        public static IEnumerable<string> MailRecipients { get; }

        public static SmtpConfiguration Smtp { get; }


        static Configuration()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["FindingImmo"]?.ConnectionString;
            MailRecipients = ConfigurationManager.AppSettings["Smtp.Recipients"]?.Split(',')?.ToList() ?? Enumerable.Empty<string>();
            Smtp = SmtpConfiguration.Instance;
        }

        public static void Bootstrap()
        {
            // Does nothing, as the class is initialised by its static constructor
            // Calling the method & referencing the class from another location is enough to bootstrap the code logic
        }
    }
}
