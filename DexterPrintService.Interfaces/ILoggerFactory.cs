namespace DexterPrintService.Interfaces
{
    public interface ILoggerFactory
    {
        ILogger Create<T>(string name) where T : class;
    }
}
