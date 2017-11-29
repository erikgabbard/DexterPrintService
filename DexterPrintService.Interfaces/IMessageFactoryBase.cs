using RabbitMQ.Client;

namespace DexterPrintService.Interfaces
{
    public interface IMessageFactoryBase
    {
        IConnection GetConnection(string hostname, int port, string username, string password);

        //void CreateExchangeQueue(IModel channel, string exchange, string queue, string routeKey);
    }
}
