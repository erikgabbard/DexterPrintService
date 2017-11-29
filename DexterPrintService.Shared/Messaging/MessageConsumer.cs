using DexterPrintService.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DexterPrintService.Shared.Messaging
{
    public class MessageConsumer : MessagingBase, IMessageConsumer
    {
        private const int timeout = 2000;

        /// <summary>
        /// Listen for messages that are delivered to the queue with the specified route key
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routeKey"></param>
        /// <returns>Returns the messages as they come in while continuing to listen for more messages.</returns>
        public IEnumerable<IPrintQueueMessage> ListenForMessages(IConnection connection, string exchange, string queue, string routeKey)
        {
            using (var channel = connection.CreateModel())
            {
                // Only prefetch one message at a time
                channel.BasicQos(0, 1, false);

                CreateExchangeQueue(channel, exchange, queue, routeKey);

                using (var subscription = new Subscription(channel, queue, false))
                {
                    PrintQueueMessage message = null;
                    var encoding = new UTF8Encoding();

                    while (channel.IsOpen)
                    {
                        var deliverySuccess = subscription.Next(timeout, out BasicDeliverEventArgs deliveryEventArgs);

                        if (!deliverySuccess)
                        {
                            continue;
                        }

                        try
                        {
                            message = JsonConvert.DeserializeObject<PrintQueueMessage>(encoding.GetString(deliveryEventArgs.Body));
                        }
                        catch (Exception ex)
                        {
                            // Message format was incorrect.  Publish to the error queue
                            var errorMessage = new PrintQueueMessage()
                            {
                                Id = Guid.NewGuid().ToString(),
                                FilePath = string.Empty,
                                OutputFile = string.Empty,
                                DeleteAfterPrint = false,
                                ErrorData = $"{ex.Message} => ORIGINAL MESSAGE => {encoding.GetString(deliveryEventArgs.Body)}"
                            };

                            var messageProducer = new MessageProducer();
                            messageProducer.PublishMessage(connection, exchange, ConfigurationManager.AppSettings["ErrorQueue"], "Errors", errorMessage);
                        }

                        // Acknowledge the message to remove it from the queue
                        channel.BasicAck(deliveryEventArgs.DeliveryTag, false);

                        // Return the messages as they come in so they can be processed
                        yield return message;
                    }
                }
            }
        }
    }
}
