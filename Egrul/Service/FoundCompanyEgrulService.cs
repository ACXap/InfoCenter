using Common.Data;
using Common.Service;
using Common.Service.Interface;
using Egrul.Data.Model;
using Egrul.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Egrul.Service
{
    public class FoundCompanyEgrulService : IFoundCompanyEgrulService
    {
        public FoundCompanyEgrulService(IRepositoryEgrul rep, ILoggerService logger)
        {
            _createFile = new CreateFileOffice("Egrul");
            _repositoryEgrul = rep;
            _logger = logger;
        }

        #region PrivateField
        private readonly IRepositoryEgrul _repositoryEgrul;
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
                    var list = _repositoryEgrul.GetCollectionCompany(query);

                    if (list == null || !list.Any())
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }
                    else
                    {
                        result.Items = list.Select(x =>
                        {
                            return new CompanyInfo()
                            {
                                Address = x.Address,
                                Director = x.Director,
                                Inn = x.Inn,
                                Ogrn = x.Ogrn,
                                Title = x.Title,
                                DateOgrn = x.DateOgrn,
                                DateRemove = x.DateRemove,
                                FullTitle = x.FullTitle,
                                Kpp = x.Kpp,
                                TokenLoadFile = x.TokenLoadFile
                            };
                        });
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                    _logger.AddLog(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }
        
        public async Task<Result<bool>> GetPdfFile(CompanyInfo company)
        {
            return await Task.Run(() =>
            {
                Result<bool> result = new Result<bool>();

                try
                {
                    var file = _createFile.CreateFileName(company.Ogrn, "pdf");

                    result.Item = _repositoryEgrul.GetFile(company.TokenLoadFile, file);

                    if (result.Item)
                    {
                        _createFile.OpenFolderFile(file);
                    }
                }
                catch (Exception ex)
                {
                    _logger.AddLog(ex.Message);
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd); 
                }

                return result;
            }).ConfigureAwait(false);
        }      
        #endregion PublicMethod
    }
}