using System;

namespace FindingImmo.Core.Infrastructure.Logging
{
    public static class Logger
    {
        private static readonly ILogger _instance = new ConsoleLogger();

        internal static ILogger Instance => _instance;

        public static void Info(string message)
        {
            Instance.Info(message);
        }

        public static void Error(string message, Exception ex = null)
        {
            Instance.Error(message, ex);
        }

        public static void Error(Exception ex)
        {
            Instance.Fatal(ex);
        }
    }
}
