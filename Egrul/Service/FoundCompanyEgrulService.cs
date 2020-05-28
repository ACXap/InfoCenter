using Common.Data;
using Common.Service;
using Egrul.Data.Model;
using Egrul.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Egrul.Service
{
    public class FoundCompanyEgrulService : IFoundCompanyEgrulService
    {
        public FoundCompanyEgrulService(IRepositoryEgrul rep)
        {
            _createFile = new CreateFileOffice("Egrul");
            _repositoryEgrul = rep;
        }

        #region PrivateField
        private readonly IRepositoryEgrul _repositoryEgrul;
        private readonly ICreateFileOfResult _createFile;
        #endregion PrivateField

        #region PublicMethod
        public async Task<Result<CompanyInfo>> FoundCompany(string query)
        {
            return await Task.Run(() =>
            {
                Result<CompanyInfo> result = new Result<CompanyInfo>();

                try
                {
                    var list = _repositoryEgrul. GetCollectionCompany(query);

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

                    if (!result.Items.Any())
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
                    // Тут будет логер
                    result.Item = false;
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd); 
                }

                return result;
            }).ConfigureAwait(false);
        }      
        #endregion PublicMethod
    }
}