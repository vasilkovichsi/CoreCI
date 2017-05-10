using System;

namespace CoreCI.Common.Logging.Interfaces
{
    /// <summary>
    /// Interface fo logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogMessage(string message);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void LogException(Exception exception);
    }
}