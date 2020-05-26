using Fssp.Repository.Data;
using Fssp.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fssp.Service
{
    public class ConvertResultToXlsx : IConvertResultToFile
    {
        public ConvertResultToXlsx(string tempFolder)
        {
            _directoryLoadFile = tempFolder;
        }

        private readonly string _directoryLoadFile;

        public string ConvertResult(IEnumerable<EntityResult> result, string fileName)
        {
            fileName = fileName.Replace("/", "_");

            var file = _directoryLoadFile + @"\" + fileName + ".xlsx";
                      
            var sourceFile = SaveTempFile(ConvertEntityToString(result), fileName);

            Convert(Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, file, sourceFile);
            return file;
        }

        private static string[] ConvertEntityToString(IEnumerable<EntityResult> result)
        {
            return result.Select(x =>
            {
                return $"{x.Name};{x.Production};{x.Details};{x.Subject};{x.Department};{x.Bailiff};{x.End}";
            }).ToArray();
        }

        private static void Convert(Microsoft.Office.Interop.Excel.XlFileFormat format, string fileName, string sourceFile)
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

                excelDoc.SaveAs(fileName, format);
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

                throw ex;
            }
        }

        private string SaveTempFile(string[] str, string fileName)
        {
            var fileTemp = _directoryLoadFile + @"\" + fileName + ".txt";

            File.WriteAllLines(fileTemp, new string[] 
            { 
                "Должник;" +
                "Исполнительное производство;" +
                "Реквизиты исполнительного документа;" +
                "Предмет исполнения, сумма непогашенной задолженности;" +
                "Отдел судебных приставов;" +
                "Судебный пристав-исполнитель;" +
                "Дата, причина окончания или прекращения ИП"
            },Encoding.Default);

            File.AppendAllLines(fileTemp, str, Encoding.Default);

            return fileTemp;
        }
    }
}