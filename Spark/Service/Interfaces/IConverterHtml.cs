namespace Spark.Service.Interfaces
{
    interface IConverterHtml
    {
        string ConvertToPdf(string html, string fileName);
        string ConvertToDocx(string html, string fileName);
    }
}