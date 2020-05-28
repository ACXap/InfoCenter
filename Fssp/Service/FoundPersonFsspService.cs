using Common.Data;
using Common.Expansion;
using Common.Service;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Repository;
using Fssp.Repository.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Fssp.Service
{
    public class FoundPersonFsspService : IFoundPersonFsspService
    {
        public FoundPersonFsspService(IRepositoryFssp repository, ISettingsService settings)
        {
            _repository = repository;

            _key = settings?.GetSettings().ApiKeyFssp;
            _createFile = new CreateFileOffice("Fssp");
        }

        #region PrivateField
        private readonly IRepositoryFssp _repository;
        private readonly ICreateFileOfResult _createFile;

        private readonly string _key;
        #endregion PrivateField

        #region PrivateMethod

        #endregion PrivateMethod

        #region PublicMethod
        public async Task<Result<RequestFoundPerson>> GetPerson(FoundPerson person)
        {
            return await Task.Run(() =>
            {
                Result<RequestFoundPerson> result = new Result<RequestFoundPerson>();
                var req = new RequestFoundPerson(person);
                result.Item = req;
                req.StartRequest();

                try
                {
                    var res = _repository.SearchPerson(ServiceConvert.ConvertFoundPersonToEntityPerson(person), _key);
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
                result.Item = req;
                req.StartRequest();

                try
                {
                    var res = _repository.SearchCompany(ServiceConvert.ConvertFoundCompanyToEntityCompany(company), _key);
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
                result.Item = req;
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
                    Thread.Sleep(5000);
                    error = string.Empty;
                    try
                    {
                        var res = _repository.Status(req.Token, _key);

                        if (string.IsNullOrEmpty(res.Exception))
                        {
                            req.StatusRequest.Progress = res.Progress.Replace("of", "из");

                            if (CheckResultStatus(res))
                            {
                                var result = _repository.Result(req.Token, _key);

                                if (string.IsNullOrEmpty(result.Exception))
                                {
                                    var str = _createFile.CreateXlsx(ServiceConvert.ConvertEntityResultsToStrings(result), req.Query.FirstField);
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

        private static bool CheckResultStatus(EntityStatus res)
        {
            if (res.Code != 0) return false;

            var counts = res.Progress.Split(new string[] { " of " }, StringSplitOptions.RemoveEmptyEntries);

            return counts[0] == counts[1];
        }

        public void GetPersonFile(string file)
        {
            _createFile.OpenFolderFile(file);
        }
        #endregion PublicMethod

        public async Task<Result<RequestFoundPerson>> ProcessingList(string file)
        {
            Result<RequestFoundPerson> result = new Result<RequestFoundPerson>();

            return await Task.Run(() =>
            {
                try
                {
                    var str = ServiceFile.ReadFile(file).Skip(1);

                    var persons = ServiceConvert.ConvertStringToEntityPerson(str);

                    var p = persons.SplitCollection(2);

                    var list = new List<RequestFoundPerson>();
                    result.Items = list;


                    int i = 1;
                    var fileName = Path.GetFileNameWithoutExtension(file);

                    foreach(var item in p)
                    {
                        var req = new RequestFoundPerson(new FoundNumber() {Number = $"{fileName}_{i++}" });
                        result.Item = req;
                        req.StartRequest();
                        list.Add(req);

                        var res = _repository.SearchGroopPerson(item, _key);
                        if (string.IsNullOrEmpty(res.Exception))
                        {
                            req.Token = res.TokenTask;
                            GetResult(req);
                        }
                        else
                        {
                            req.ErrorRequest(res.Exception);
                        }

                        Thread.Sleep(5000);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                }

                return result;

            }).ConfigureAwait(false);
        }
    }
}