using Common.Data;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Repository;
using Fssp.Repository.Data;
using Fssp.Service.Interface;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Fssp.Service
{
    public class FoundPersonFsspService : IFoundPersonFsspService
    {
        public FoundPersonFsspService(IRepositoryFssp repository, ISettingsService settings)
        {
            _repository = repository;

            var curDir = Directory.GetCurrentDirectory();
            _directoryLoadFile = curDir + @"\Fssp";
            Directory.CreateDirectory(_directoryLoadFile);

            _converter = new ConvertResultToXlsx(_directoryLoadFile);
            _serviceFio = new ServiceFio();

            _key = settings?.GetSettings().ApiKeyFssp;
        }

        #region PrivateField
        private readonly IRepositoryFssp _repository;
        private readonly IConvertResultToFile _converter;
        private readonly IServiceFio _serviceFio;

        private readonly string _key;
        private readonly string _directoryLoadFile;
        #endregion PrivateField

        #region PrivateMethod
        private EntityPerson GetEntityPerson(FoundPerson person)
        {
            var fio = _serviceFio.GetFio(person.Fio);
            return new EntityPerson()
            {
                Lastname = fio[0],
                Firstname = fio[1],
                Secondname = fio[2],
                Birthdate = person.Date,
                Region = person.Region.Id
            };
        }

        private EntityCompany GetEntityCompany(FoundCompany company)
        {
            return new EntityCompany()
            {
                Address = company.Address,
                Name = company.Name,
                Region = company.Region.Id
            };
        }

        #endregion PrivateMethod

        #region PublicMethod
        public async Task<Result<RequestFoundPerson>> GetPerson(FoundPerson person)
        {
            return await Task.Run(() =>
            {
                Result<RequestFoundPerson> result = new Result<RequestFoundPerson>();
                var req = new RequestFoundPerson(person);
                result.Object = req;
                req.StartRequest();

                try
                {
                    var res = _repository.SearchPerson(GetEntityPerson(person), _key);
                    if (string.IsNullOrEmpty(res.Exception))
                    {
                        req.Token = res.TokenTask;
                        GetResult(req);
                    }
                    else
                    {
                        req.ErrorRequest(res.Exception);
                    }
                }
                catch (Exception ex)
                {
                    req.ErrorRequest(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }

        public async Task<Result<RequestFoundPerson>> GetCompany(FoundCompany company)
        {
            return await Task.Run(() =>
            {
                Result<RequestFoundPerson> result = new Result<RequestFoundPerson>();
                var req = new RequestFoundPerson(company);
                result.Object = req;
                req.StartRequest();

                try
                {
                    var res = _repository.SearchCompany(GetEntityCompany(company), _key);
                    if (string.IsNullOrEmpty(res.Exception))
                    {
                        req.Token = res.TokenTask;
                        GetResult(req);
                    }
                    else
                    {
                        req.ErrorRequest(res.Exception);
                    }
                }
                catch (Exception ex)
                {
                    req.ErrorRequest(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }

        public async Task<Result<RequestFoundPerson>> GetNumber(string number)
        {
            return await Task.Run(() =>
            {
                Result<RequestFoundPerson> result = new Result<RequestFoundPerson>();
                var req = new RequestFoundPerson(new FoundNumber() { Number = number });
                result.Object = req;
                req.StartRequest();

                try
                {
                    var res = _repository.SearchIp(number, _key);
                    if (string.IsNullOrEmpty(res.Exception))
                    {
                        req.Token = res.TokenTask;
                        GetResult(req);
                    }
                    else
                    {
                        req.ErrorRequest(res.Exception);
                    }
                }
                catch (Exception ex)
                {
                    req.ErrorRequest(ex.Message);
                }

                return result;
            }).ConfigureAwait(false);
        }
        public void GetResult(RequestFoundPerson req)
        {
            Task.Run(() =>
            {
                int count = 60;
                string error = null;

                while (count-- > 0)
                {
                    error = string.Empty;

                    try
                    {
                        Thread.Sleep(2000);
                        var res = _repository.Status(req.Token, _key);

                        if (string.IsNullOrEmpty(res.Exception))
                        {
                            req.StatusRequest.Progress = res.Progress.Replace("of", "из");
                            if (res.Code == 0 && res.Progress == "1 of 1")
                            {
                                var result = _repository.Result(req.Token, _key);

                                if (string.IsNullOrEmpty(result.Exception))
                                {
                                    var str = _converter.ConvertResult(result.CollectionQuery[0].CollectionResult, req.Query.FirstField);
                                    req.FileResult = str;
                                    req.StopRequest();
                                    break;
                                }
                                else
                                {
                                    error = result.Exception;
                                }
                            }
                        }
                        else
                        {
                            error = res.Exception;
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }

                    error = "Время ожидания истекло.";
                }

                req.ErrorRequest(error);
            });
        }
        public void GetPersonFile(string file)
        {
            System.Diagnostics.Process.Start("explorer", @"/select, " + file);
        }
        #endregion PublicMethod
    }
}