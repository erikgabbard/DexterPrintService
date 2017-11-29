using DexterPrintService.Interfaces;
using DexterPrintService.Shared.Messaging;
using Ninject;
using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace DexterPrintService
{
    public class PrintService : ServiceControl
    {
        const int taskTimeout = 5000;

        readonly Task processMessages;
        readonly IPrintPDF pdfPrinter;
        readonly IMessageConsumer messageConsumer;
        readonly ILoggerFactory loggerFactory;

        CancellationTokenSource cancellationTokenSource;

        string messagingHost;
        int messagingPort;
        string hostUsername;
        string hostPassword;
        string hostExchange;
        string hostQueue;
        string routeKey;
        string printerName;

        public PrintService(IKernel kernel)
        {
            messageConsumer = kernel.Get<IMessageConsumer>();
            pdfPrinter = kernel.Get<IPrintPDF>();
            loggerFactory = kernel.Get<ILoggerFactory>();

            cancellationTokenSource = new CancellationTokenSource();

            processMessages = new Task(MonitorPrintQueue, cancellationTokenSource.Token);
        }

        public bool Start(HostControl hostControl)
        {
            messagingHost = ConfigurationManager.AppSettings["RabbitMQHost"];
            int.TryParse(ConfigurationManager.AppSettings["RabbitMQPort"], out messagingPort);
            hostUsername = ConfigurationManager.AppSettings["HostUsername"];
            hostPassword = ConfigurationManager.AppSettings["HostPassword"];
            hostExchange = ConfigurationManager.AppSettings["HostExchange"];
            hostQueue = ConfigurationManager.AppSettings["HostQueue"];
            routeKey = ConfigurationManager.AppSettings["RouteKey"];
            printerName = ConfigurationManager.AppSettings["PrinterName"];

            processMessages.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {

            using (cancellationTokenSource)
            {
                cancellationTokenSource.Cancel();
            }

            processMessages.Wait(taskTimeout);
            cancellationTokenSource = null;

            return true;
        }

        private void MonitorPrintQueue()
        {
            var errorLog = loggerFactory.Create<MessageConsumer>("ErrorLog");
            var successLog = loggerFactory.Create<MessageConsumer>("SuccessLog");
            IConnection connection = null;

            successLog.Debug("Listening for print messages");

            connection = messageConsumer.GetConnection(messagingHost, messagingPort, hostUsername, hostPassword);
            foreach (var printMessage in messageConsumer.ListenForMessages(connection, hostExchange, hostQueue, routeKey))
            {
                try
                {
                    if (printMessage == null)
                    {
                        throw new InvalidOperationException("Message format was incorrect.");
                    }
                    pdfPrinter.PrintPDF(printMessage.FilePath, printMessage.OutputFile, printerName);
                    successLog.Success(printMessage.ToString());
                }
                catch (InvalidOperationException ex)
                {
                    errorLog.Error($"Failure to print:{Environment.NewLine}{printMessage?.ToString()}", ex);
                }
            }
        }
    }
}
