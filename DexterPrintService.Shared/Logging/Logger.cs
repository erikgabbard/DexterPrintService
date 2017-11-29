using NLog;
using System;

namespace DexterPrintService.Shared.Logging
{
    public class Logger : Interfaces.ILogger
    {
        private readonly NLog.Logger logger;

        public string Name
        {
            get { return logger.Name; }
        }

        public Logger(Type type, string name)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }

            logger = LogManager.GetLogger(name);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(ex, message);
        }

        public void Success(string message)
        {
            logger.Info(message);
        }
    }
}
