using Common.Data;
using Common.Service;
using Common.Service.Interface;
using Spark.Data.Model;
using Spark.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spark.Service
{
    public class FoundCompanyService : IFoundCompanySparkService
    {
        public FoundCompanyService(IRepositorySpark rep, ILoggerService logger)
        {
            _createFile = new CreateFileOffice("Spark");
            _repositorySpark = rep;
            _logger = logger;
        }

        #region PrivateField
        private readonly IRepositorySpark _repositorySpark;
        private readonly ICreateFileOfResult _createFile;
        private readonly ILoggerService _logger;
        #endregion PrivateField

        #region PublicMethod
        public async Task<Result<CompanyInfo>> FoundCompany(string query)
        {
            return await Task.Run(() =>
            {
                Result<CompanyInfo> result = new Result<CompanyInfo>();

                try
                {
                    var list = _repositorySpark.FoundCompany(query);

                    result.Items = list.Select(x =>
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

                    if (!result.Items.Any())
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd);
                    _logger.AddLog(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }
        public async Task<Result<bool>> GetFileCompany(CompanyInfo company)
        {
            return await Task.Run(() =>
            {
                Result<bool> result = new Result<bool>();

                try
                {
                    var str = _repositorySpark.GetCompany(company.Link);

                    var resultDoc = _createFile.CreateDocx(str.Html, company.Ogrn);
                    var resultPdf = _createFile.CreatePdf(str.Html, company.Ogrn);

                    _createFile.OpenFolderFile(resultDoc);

                    result.Item = !string.IsNullOrEmpty(resultDoc) && !string.IsNullOrEmpty(resultPdf);
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd);
                    _logger.AddLog(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }
        #endregion PublicMethod
    }
}