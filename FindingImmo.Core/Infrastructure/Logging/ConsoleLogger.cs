using System;

namespace FindingImmo.Core.Infrastructure.Logging
{
    internal sealed class ConsoleLogger : ILogger
    {
        public void Fatal(Exception ex)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            Log(ex.ToString());
        }

        public void Error(string message, Exception ex = null)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (ex != null)
                message += (Environment.NewLine + ex.ToString());

            Log(message);
        }

        public void Info(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            Log(message);
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
