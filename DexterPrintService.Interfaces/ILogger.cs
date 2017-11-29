using System;

namespace DexterPrintService.Interfaces
{
    public interface ILogger
    {
        string Name { get; }
        void Debug(string message);
        void Error(string message, Exception ex);
        void Success(string message);
    }
}
