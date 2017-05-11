using System;
using System.Reflection;
using CoreCI.Common.Logging.Interfaces;
using log4net;
using log4net.Config;

namespace CoreCI.Common.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logger;
        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(typeof(Log4NetLogger));
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            BasicConfigurator.Configure(logRepository);
        }

        public void LogMessage(string message)
        {
            _logger.Info(message);
        }

        public void LogException(Exception exception)
        {
            _logger.Error(exception);
        }
    }
}