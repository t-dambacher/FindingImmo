using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FindingImmo.Core.Infrastructure
{
    public static class Configuration
    {
        private static IConfigurationRoot Instance { get; } = Configure();

        private static IConfigurationRoot Configure()
        {
            return new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .AddXmlFile(GetConfigurationFileName(), optional: true)
                .Build();
        }

        private static string GetConfigurationFileName()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
                return "FindingImmo.Console.exe.config"; // todo: should be "Web.config" when running as a website

            return Path.GetFileName(assembly.Location) + ".config";
        }

        public static string ConnectionString
        {
            get
            {
                return @"Data Source=..\..\..\Database.db";
            }
        }

        public static IEnumerable<string> MailRecipients
        {
            get
            {
                return ConfigurationManager.AppSettings["mailRecipients"]?.Split(',')?.ToList() ?? Enumerable.Empty<string>();
            }
        }

        public static void Bootstrap()
        {
            // Does nothing, as the class is initialised by its static constructor
            // Calling the method & referencing the class from another location is enough to bootstrap the code logic
        }
    }
}
