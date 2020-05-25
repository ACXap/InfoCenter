using Common.Data;
using Common.Data.Enum;
using Egrul.Data.Model;
using Egrul.Repository;
using Egrul.Service.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Egrul.Service
{
    public class FoundCompanyEgrulService : IFoundCompanyEgrulService
    {
        private string _directoryLoadFile;

        public FoundCompanyEgrulService(IRepositoryEgrul rep)
        {
            var curDir = Directory.GetCurrentDirectory();
            _directoryLoadFile = curDir + @"\Egrul";
            Directory.CreateDirectory(_directoryLoadFile);

            _repositoryEgrul = rep;
        }

        #region PrivateField
        private readonly IRepositoryEgrul _repositoryEgrul;
        #endregion PrivateField

        #region PublicMethod
        public async Task<Result<CompanyInfo>> GetCollectionCompany(string query)
        {
            return await Task.Factory.StartNew(() =>
            {
                Result<CompanyInfo> result = new Result<CompanyInfo>();

                try
                {
                    var list = _repositoryEgrul. GetCollectionCompany(query);

                    result.Objects = list.Select(x =>
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

                    if (!result.Objects.Any())
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
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
                    var file = _directoryLoadFile + @"\" + company.Ogrn + ".pdf";
                    var a = _repositoryEgrul.GetFile(company.TokenLoadFile, file);

                    if (a)
                    {
                        System.Diagnostics.Process.Start("explorer", @"/select, " + file);
                    }

                    result.Object = a;
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