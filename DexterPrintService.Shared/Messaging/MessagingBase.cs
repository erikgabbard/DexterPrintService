using DexterPrintService.Interfaces;
using RabbitMQ.Client;

namespace DexterPrintService.Shared.Messaging
{
    public class MessagingBase : IMessageFactoryBase
    {
        /// <summary>
        /// Creates the RabbitMQ connection.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IConnection GetConnection(string hostname, int port, string username, string password)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port,
                UserName = username,
                Password = password
            };

            return factory.CreateConnection();
        }

        /// <summary>
        /// Creates the exchange and queue and binds them using the routing key.
        /// If either already exist, RabbitMQ will use the existing instead of recreating them.
        /// </summary>
        /// <param name="channel">The AMQP model that defines the channel of communiction of messages.</param>
        /// <param name="exchange">The name of the RabbitMQ exchange to bind with the queue.</param>
        /// <param name="queue">The name of the RabbitMQ queue to deliver messages to.</param>
        /// <param name="routeKey">The key for the particular message type that the consumer will listen for.</param>
        protected void CreateExchangeQueue(IModel channel, string exchange, string queue, string routeKey)
        {
            channel.ExchangeDeclare(
                        exchange: exchange,
                        type: "direct",
                        durable: true,
                        autoDelete: false,
                        arguments: null);

            channel.QueueDeclare(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(queue, exchange, routeKey);
        }
    }
}
