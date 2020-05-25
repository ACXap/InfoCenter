using Spark.Service.Interfaces;
using System;
using System.IO;

namespace Spark.Service
{
    public class ConverterHtmlWord : IConverterHtml
    {
        public ConverterHtmlWord(string tempFolder)
        {
            _directoryLoadFile = tempFolder;
        }
        private string _directoryLoadFile;

        public string ConvertToDocx(string html, string fileName)
        {
            var file = _directoryLoadFile + @"\" + fileName + ".docx";
            var sourceFile = SaveTempFile(html, fileName);

            Convert(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault, file, sourceFile);
            return file;
        }

        public string ConvertToPdf(string html, string fileName)
        {
            var file = _directoryLoadFile + @"\" + fileName + ".pdf";
            var sourceFile = SaveTempFile(html, fileName);

            Convert(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, file, sourceFile);
            return file;
        }

        private string SaveTempFile(string html, string fileName)
        {
            var fileTemp = _directoryLoadFile + @"\" + fileName + ".html";
            File.WriteAllText(fileTemp, html);

            return fileTemp;
        }

        private void Convert(Microsoft.Office.Interop.Word.WdSaveFormat format, string fileName, string sourceFile)
        {
            Microsoft.Office.Interop.Word.Document wordDoc = null;
            Microsoft.Office.Interop.Word.Application word = null;

            try
            {
                word = new Microsoft.Office.Interop.Word.Application
                {
                    Visible = false
                };
                wordDoc = word.Documents.Open(sourceFile, false);

                wordDoc.SaveAs2(FileName: fileName, FileFormat: format);
                wordDoc.Close();
                word.Quit();
            }
            catch (Exception ex)
            {
                try
                {
                    wordDoc?.Close();
                    word?.Quit();
                }
                catch { }

                throw ex;
            }
        }
    }
}