using System;

namespace FindingImmo.Core.Infrastructure
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception ex);
        void Error(string message, Exception ex = null);
    }
}
