using DexterPrintService.Interfaces;
using Spire.Pdf;
using Spire.Pdf.Exceptions;
using System;
using System.IO;

namespace DexterPDFPrinter
{
    public class PDFPrinter : IPrintPDF
    {
        public void PrintPDF(string filePath, string outputPath, string printerName, int copies = 1)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new ArgumentException("Path to file not specified.", filePath);
                }

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The specified file was not found.", filePath);
                }

                using (var document = new PdfDocument())
                {
                    document.LoadFromFile(filePath);
                    document.PrintSettings.PrinterName = printerName;
                    document.PrintSettings.Copies = (short)copies;

                    if (!string.IsNullOrEmpty(outputPath))
                    {
                        document.PrintSettings.PrintToFile(outputPath); 
                    }

                    document.Print();
                }
            }
            catch (PdfConformanceException conformException)
            {
                throw new Exception(conformException.Message, conformException);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to print {Path.GetFileName(filePath)}.", ex);
            }
        }
    }
}
