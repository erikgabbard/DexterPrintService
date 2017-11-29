using DexterPrintService.Shared.Messaging;
using System;
using System.Configuration;

namespace ConsumeMessageTest
{
    public class Consumer
    {
        public static void Main()
        {
            Console.Write("Enter Exchange: ");
            var exchange = Console.ReadLine();

            Console.Write("Enter Queue: ");
            var queue = Console.ReadLine();

            Console.Write("Enter Route Key: ");
            var routeKey = Console.ReadLine();

            Console.WriteLine($"Listening for messages on {queue}");

            var messageConsumer = new MessageConsumer();
            var connection = messageConsumer.GetConnection(ConfigurationManager.AppSettings["RabbitMQHost"],
                                                            Convert.ToInt32(ConfigurationManager.AppSettings["RabbitMQPort"]),
                                                            ConfigurationManager.AppSettings["HostUsername"],
                                                            ConfigurationManager.AppSettings["HostPassword"]);

            foreach (var message in messageConsumer.ListenForMessages(connection, "Dexter Printing", queue, "Test"))
            {
                Console.WriteLine(" [x] Received {0}", message.ToString());
            }

            connection.Close();
        }
    }
}
