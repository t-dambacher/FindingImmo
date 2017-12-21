using System;

namespace FindingImmo.Core.Infrastructure
{
    sealed internal class ConsoleLogger : ILogger
    {
        public void Error(Exception ex)
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
                message += (message + Environment.NewLine + ex.ToString());

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
