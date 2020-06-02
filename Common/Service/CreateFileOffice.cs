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

        #region PrivateField
        private readonly object _lock = new object();
        private readonly string _folder;

        #endregion PrivateField

        #region PrivateMethod
        private void CreateFolder()
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }
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

        private static string RemoveInvalidChar(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("Имя файла нулл", nameof(fileName));

            var charsInvalid = Path.GetInvalidFileNameChars().Union(Path.GetInvalidPathChars());

            foreach (char c in charsInvalid)
            {
                fileName = fileName.Replace(c.ToString(), "_");
            }

            return fileName;
        }
        private string CreateTxt(IEnumerable<string> text, string fileName)
        {
            var fileTemp = CreateFileName(fileName, "txt");
            File.WriteAllLines(fileTemp, text, Encoding.Default);
            return fileTemp;
        }

        private string AppendTxt(IEnumerable<string> text, string fileName)
        {
            var fileTemp = CreateFileName(fileName, "txt");
            if (File.Exists(fileTemp))
            {
                File.AppendAllLines(fileTemp, text.Skip(1), Encoding.Default);
            }
            else
            {
                File.WriteAllLines(fileTemp, text, Encoding.Default);
            }
            
            return fileTemp;
        }

        private string CreateHtml(string text, string fileName)
        {
            var fileTemp = CreateFileName(fileName, "html");
            File.WriteAllText(fileTemp, text);
            return fileTemp;
        }

        private static string GetUniqueOnlyFileName(string file)
        {
            var f = Path.GetFileNameWithoutExtension(file);

            if (File.Exists(file))
            {
                f = $"{f}_{DateTime.Now:yyMMddHHmmssfff}";
            }

            return f;
        }

        #endregion PrivateMethod

        #region PublicMethod
        public string CreateDocx(string text, string fileName)
        {
            CreateFolder();
            var file = fileName;

            var sourceFile = CreateHtml(text, file);

            file = CreateFileName(file, "docx");

            ConvertWord(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDocumentDefault, file, sourceFile);
            return file;
        }

        public string CreatePdf(string text, string fileName)
        {
            CreateFolder();
            var file = fileName;

            var sourceFile = CreateHtml(text, file);

            file = CreateFileName(file, "pdf");

            ConvertWord(Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF, file, sourceFile);
            return file;
        }

        public string CreateXlsx(IEnumerable<string> text, string fileName)
        {
            CreateFolder();
            var file = fileName;

            var sourceFile = CreateTxt(text, file);

            file = CreateFileName(file, "xlsx");

            ConvertExcel(file, sourceFile);
            return file;
        }

        public string AppendXlsx(IEnumerable<string> text, string fileName)
        {
            CreateFolder();
            var file = fileName;

            lock (_lock)
            {
                var sourceFile = AppendTxt(text, file);

                file = CreateFileName(file, "xlsx");

                ConvertExcel(file, sourceFile);
            }

            return file;
        }

        public string CreateFileName(string fileName, string expansionFile)
        {
            var f = Path.GetFileNameWithoutExtension(fileName);

            f = RemoveInvalidChar(f);

            var file = Path.Combine(_folder, f + $".{expansionFile}");

            file = GetUniqueOnlyFileName(file);

            return Path.Combine(_folder, file + $".{expansionFile}");
        }

        public void OpenFolderFile(string file)
        {
            System.Diagnostics.Process.Start("explorer", @"/select, " + file);
        }
        #endregion PublicMethod
    }
}