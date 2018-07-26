using System;

namespace FindingImmo.Core.Infrastructure.Logging
{
    public static class Logger
    {
        internal static ILogger Instance { get; } = new ConsoleLogger();

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
