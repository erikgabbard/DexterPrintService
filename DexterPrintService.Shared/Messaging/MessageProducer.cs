using DexterPrintService.Interfaces;
using System;
using RabbitMQ.Client;

namespace DexterPrintService.Shared.Messaging
{
    public class MessageProducer : MessagingBase, IMessageProducer
    {
        /// <summary>
        /// Publish message to specified RabbitMQ queue
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routeKey"></param>
        /// <param name="message"></param>
        public void PublishMessage(IConnection connection, string exchange, string queue, string routeKey, IPrintQueueMessage message)
        {
            using (var channel = connection.CreateModel())
            {
                CreateExchangeQueue(channel, exchange, queue, routeKey);

                var body = message.Serialize();
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: exchange,
                    routingKey: routeKey,
                    basicProperties: properties,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message.ToString());
            }
        }
    }
}
