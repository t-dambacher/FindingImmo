using System;
using System.Configuration;

namespace FindingImmo.Core.Infrastructure
{
    public sealed class SmtpConfiguration
    {
        public string Sender { get; }
        public string Host { get; }
        public int Port { get; }
        public string UserName { get; }
        public string Password { get; }

        public static SmtpConfiguration Instance { get; } = new SmtpConfiguration();

        private SmtpConfiguration()
        {
            Sender = ConfigurationManager.AppSettings["Smtp.Sender"];
            Host = ConfigurationManager.AppSettings["Smtp.Host"];
            Port = int.TryParse(ConfigurationManager.AppSettings["Smtp.Port"], out int result) ? result : throw new InvalidOperationException($"Unable to parse the 'Smtp.Port' configuration value.");
            UserName = ConfigurationManager.AppSettings["Smtp.UserName"];
            Password = ConfigurationManager.AppSettings["Smtp.Password"];
        }
    }
}
