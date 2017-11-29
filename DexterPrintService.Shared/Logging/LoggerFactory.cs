using DexterPrintService.Interfaces;

namespace DexterPrintService.Shared.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        public ILogger Create<T>(string name) where T : class
        {
            return new Logger(typeof(T), name);
        }
    }
}
