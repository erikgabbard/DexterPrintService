using DexterPrintService.Interfaces;
using Ninject.Modules;
using DexterPDFPrinter;
using DexterPrintService.Shared.Messaging;
using DexterPrintService.Shared.Logging;

namespace DexterPrintService
{
    class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPrintPDF>().To<PDFPrinter>().InSingletonScope();
            Bind<IMessageConsumer>().To<MessageConsumer>().InSingletonScope();
            Bind<IMessageProducer>().To<MessageProducer>().InSingletonScope();
            Bind<ILoggerFactory>().To<LoggerFactory>().InSingletonScope();
        }
    }
}
