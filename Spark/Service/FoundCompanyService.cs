using Common.Data;
using Common.Data.Enum;
using Spark.Data.Model;
using Spark.Repository;
using Spark.Service.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Service
{
    public class FoundCompanyService : IFoundCompanySparkService
    {
        private string _directoryLoadFile;

        public FoundCompanyService(IRepositorySpark rep)
        {
            var curDir = Directory.GetCurrentDirectory();
            _directoryLoadFile = curDir + @"\Spark";
            Directory.CreateDirectory(_directoryLoadFile);

            _repositorySpark = rep;
            _converterHtml = new ConverterHtmlWord(_directoryLoadFile);
        }

        #region PrivateField
        private readonly IRepositorySpark _repositorySpark;
        private readonly IConverterHtml _converterHtml;
        #endregion PrivateField

        #region PublicMethod
        public async Task<Result<CompanyInfo>> GetCollectionCompany(string query)
        {
            return await Task.Run(() =>
            {
                Result<CompanyInfo> result = new Result<CompanyInfo>();

                try
                {
                    var list = _repositorySpark.GetCollectionCompany(query);

                    result.Objects = list.Select(x =>
                    {
                        return new CompanyInfo()
                        {
                            Address = x.Address,
                            Director = x.Director,
                            Inn = x.Inn,
                            Link = x.Link,
                            Ogrn = x.Ogrn,
                            Title = x.Title
                        };
                    });

                    if (!result.Objects.Any())
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd);
                    // Тут будет логер
                }

                return result;
            });
        }
        public async Task<Result<bool>> GetPdfFile(CompanyInfo company)
        {
            return await Task.Run(() =>
            {
                Result<bool> result = new Result<bool>();

                try
                {
                    var str = _repositorySpark.GetCompany(company.Link);

                    var resultDoc = _converterHtml.ConvertToDocx(str.Html, company.Ogrn);
                    var resultPdf = _converterHtml.ConvertToPdf(str.Html, company.Ogrn);

                    System.Diagnostics.Process.Start("explorer", @"/select, " + resultDoc);

                    result.Object = !string.IsNullOrEmpty(resultDoc) && !string.IsNullOrEmpty(resultPdf);
                }
                catch (Exception ex)
                {
                    // Тут будет логер
                    result.Object = false;
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd);
                }

                return result;
            });
        }
        #endregion PublicMethod
    }
}