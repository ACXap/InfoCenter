using Common.Data;
using Common.Data.Enum;
using Spark.Data.Model;
using Spark.Repository;
using Spark.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Spark.Service
{
    public class LoadCompanyService : ILoadCompanyService
    {
        public LoadCompanyService(IRepositorySpark repositorySpark)
        {
            _repository = repositorySpark;
        }

        #region PrivateField
        private readonly IRepositorySpark _repository;
        #endregion PrivateField

        #region PublicMethod
        public async Task<Result<Company>> GetCompany(string query)
        {
            return await Task.Factory.StartNew(() =>
            {
                Result<Company> result = new Result<Company>();

                try
                {
                    var c = _repository.GetCompany(query);

                    if (c != null)
                    {
                        result.Object = new Company()
                        {
                            //Title = c.FullTitle,
                            CompanyMainInfo = new CompanyMainInfo()
                            //{
                            //    DateFirstRegistration = c.DateFirstRegistration,
                            //    FullTitle = c.FullTitle,
                            //    Address = c.Address,
                            //    Director = c.Director,
                            //    Email = c.Email,
                            //    Inn = c.Inn,
                            //    MainAction = c.MainAction,
                            //    Ogrn = c.Ogrn,
                            //    Okopf = c.Okopf,
                            //    Phone = c.Phone,
                            //    Status = c.Status,
                            //    TitleEnglish = c.TitleEnglish,
                            //    Web = c.Web
                            //}
                        };
                    }
                    else
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
        #endregion PublicMethod
    }
}