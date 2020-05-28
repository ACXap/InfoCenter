using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Service
{
    public class CreateFileOffice : ICreateFileOfResult
    {
        public CreateFileOffice(string path)
        {
            var curDir = Directory.GetCurrentDirectory();
            _folder = Path.Combine(curDir + $"\\{path}");
            CreateFolder();
        }

        private readonly string _folder;

        public string CreateDocx(string text, string fileName)
        {
            CreateFolder();

            var file = RemoveInvalidChar(fileName);

            var sourceFile = CreateHtml(text, file, _folder);

            file = CreateFileName(fileName, "docx");

            ConvertWord(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault, file, sourceFile);
            return file;
        }

        public string CreatePdf(string text, string fileName)
        {
            CreateFolder();

            var file = RemoveInvalidChar(fileName);

            var sourceFile = CreateHtml(text, file, _folder);

            file = CreateFileName(fileName, "pdf");

            ConvertWord(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, file, sourceFile);
            return file;
        }

        public string CreateXlsx(IEnumerable<string> text, string fileName)
        {
            CreateFolder();

            var file = RemoveInvalidChar(fileName);

            var sourceFile = CreateTxt(text, file, _folder);

            file = CreateFileName(fileName, "xlsx");

            ConvertExcel(file, sourceFile);
            return file;
        }

        public string CreateFileName(string fileName, string expansionFile)
        {
            return Path.Combine(_folder, fileName + $".{expansionFile}");
        }

        public void OpenFolderFile(string file)
        {
            System.Diagnostics.Process.Start("explorer", @"/select, " + file);
        }

        private static string RemoveInvalidChar(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Имя файла нулл",nameof(fileName));

            var charsInvalid = Path.GetInvalidFileNameChars().Union(Path.GetInvalidPathChars());

            foreach (char c in charsInvalid)
            {
                fileName = fileName.Replace(c.ToString(), "");
            }

            return fileName;
        }

        private static string CreateTxt(IEnumerable<string> text, string fileName, string path)
        {
            var fileTemp = Path.Combine(path, fileName + ".txt");
            File.WriteAllLines(fileTemp, text, Encoding.Default);
            return fileTemp;
        }

        private static string CreateHtml(string text, string fileName, string path)
        {
            var fileTemp = Path.Combine(path, fileName + ".html");
            File.WriteAllText(fileTemp, text);
            return fileTemp;
        }

        private static void ConvertExcel(string fileName, string sourceFile)
        {
            Microsoft.Office.Interop.Excel.Workbook excelDoc = null;
            Microsoft.Office.Interop.Excel.Application excel = null;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                excelDoc = excel.Workbooks.Open(sourceFile, ReadOnly: true, Delimiter: ";", Format: 6, Origin: Microsoft.Office.Interop.Excel.XlPlatform.xlWindows);
                foreach (Microsoft.Office.Interop.Excel.Worksheet currentSheet in excelDoc.Worksheets)
                {
                    currentSheet.Columns.AutoFit();
                }

                excelDoc.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                excel.DisplayAlerts = true;
                excelDoc.Close();
                excel.Quit();
            }
            catch (Exception ex)
            {
                try
                {
                    excel.DisplayAlerts = true;
                    excelDoc?.Close();
                    excel?.Quit();
                }
                catch { }

                throw;
            }
        }

        private static void ConvertWord(Microsoft.Office.Interop.Word.WdSaveFormat format, string fileName, string sourceFile)
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

                throw;
            }
        }

        private void CreateFolder()
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
        }
    }
}