using RabbitMQ.Client;
using System.Collections.Generic;

namespace DexterPrintService.Interfaces
{
    public interface IMessageConsumer : IMessageFactoryBase
    {
        IEnumerable<IPrintQueueMessage> ListenForMessages(IConnection connection, string exchange, string queue, string routeKey);
    }
}
