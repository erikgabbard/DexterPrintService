using DexterPrintService.Shared;
using DexterPrintService.Shared.Messaging;
using System;
using System.Configuration;

namespace PublishMessageTest
{
    public class Producer
    {
        public static void Main()
        {
            Console.Write("Enter Exchange: ");
            var exchange = Console.ReadLine();

            Console.Write("Enter Queue: ");
            var queue = Console.ReadLine();

            Console.Write("Enter Route Key: ");
            var routeKey = Console.ReadLine();

            Console.Write("Enter # to publish: ");
            var count = Convert.ToInt32(Console.ReadLine());

            var messageProducer = new MessageProducer();
            var connection = messageProducer.GetConnection(ConfigurationManager.AppSettings["RabbitMQHost"],
                                                            Convert.ToInt32(ConfigurationManager.AppSettings["RabbitMQPort"]),
                                                            ConfigurationManager.AppSettings["HostUsername"],
                                                            ConfigurationManager.AppSettings["HostPassword"]);

            for (int i = 0; i < count; i++)
            {
                var outputFile = $"testfile_{i}.xps";
                var message = new PrintQueueMessage()
                {
                    Id = Guid.NewGuid().ToString(),
                    FilePath = @"C:\Users\Erik Gabbard\Desktop\test.pdf",
                    OutputFile = @"C:\Sample Output\" + outputFile,
                    DeleteAfterPrint = false
                };

                messageProducer.PublishMessage(connection, exchange, queue, routeKey, message);
                Console.WriteLine(" [x] Sent {0}", message.ToString());
            }

            connection.Close();

            Console.WriteLine(" Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
