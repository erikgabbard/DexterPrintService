using RabbitMQ.Client;

namespace DexterPrintService.Interfaces
{
    public interface IMessageProducer : IMessageFactoryBase
    {
        void PublishMessage(IConnection connection, string exchange, string queue, string routeKey, IPrintQueueMessage message);
    }
}
