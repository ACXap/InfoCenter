using System.Collections.Generic;

namespace Common.Service
{
    public interface ICreateFileOfResult
    {
        string CreateDocx(string text, string fileName);
        string CreatePdf(string text, string fileName);
        string CreateXlsx(IEnumerable<string> text, string fileName);

        string CreateFileName(string fileName, string expansionFile);
        void OpenFolderFile(string file);
        string AppendXlsx(IEnumerable<string> text, string fileName);
    }
}