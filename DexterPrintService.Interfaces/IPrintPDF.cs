namespace DexterPrintService.Interfaces
{
    public interface IPrintPDF
    {
        void PrintPDF(string filePath, string outputPath, string printerName, int copies = 1);
    }
}
