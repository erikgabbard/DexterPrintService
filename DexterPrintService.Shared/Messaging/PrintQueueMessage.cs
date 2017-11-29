using DexterPrintService.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace DexterPrintService.Shared.Messaging
{
    /// <summary>
    /// Data to be used as messages on the RabbitMQ queue
    /// </summary>
    public class PrintQueueMessage : IPrintQueueMessage
    {
        public string Id { get; set; }

        public string FilePath { get; set; }

        public string OutputFile { get; set; }

        public bool DeleteAfterPrint { get; set; }

        public string ErrorData { get; set; }

        /// <summary>
        /// Converts the object to a JSON string and then encodes the string as a byte array
        /// so that it can be published on the RabbitMQ queue.
        /// </summary>
        /// <returns>Byte array of the JSON string.</returns>
        public byte[] Serialize() => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
