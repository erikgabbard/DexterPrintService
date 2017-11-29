namespace DexterPrintService.Interfaces
{
    public interface IPrintQueueMessage
    {
        string Id { get; set; }

        /// <summary>
        /// UNC path where the file to be printed has been delivered.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// UNC path to use for the ouput file if printing to a file.
        /// </summary>
        string OutputFile { get; set; }

        /// <summary>
        /// Flag to signal the source file (this.FilePath) should be deleted after
        /// successfully printing
        /// </summary>
        bool DeleteAfterPrint { get; set; }

        /// <summary>
        /// Any information pertaining to a message that couldn't be processed.
        /// </summary>
        string ErrorData { get; set; }

        /// <summary>
        /// Convert the message object to a byte array to send to RabbitMQ
        /// </summary>
        /// <returns>
        /// Byte array representing the message
        /// </returns>
        byte[] Serialize();
    }
}
