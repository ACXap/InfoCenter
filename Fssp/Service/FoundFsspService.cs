using Common.Data;
using Common.Expansion;
using Common.Service;
using Common.Settings.Service;
using Fssp.Data;
using Fssp.Repository;
using Fssp.Repository.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Fssp.Service
{
    public class FoundFsspService : IFoundFsspService
    {
        public FoundFsspService(IRepositoryFssp repository, ISettingsService settings)
        {
            _repository = repository;

            _key = settings?.GetSettings().ApiKeyFssp;
            _createFile = new CreateFileOffice("Fssp");

            BindingOperations.EnableCollectionSynchronization(CollectionRequest, _personCollectionLock);
        }

        #region PublicProperties
        public ObservableCollection<RequestFound> CollectionRequest { get; } = new ObservableCollection<RequestFound>();
        #endregion PublicProperties

        #region PrivateField
        private readonly IRepositoryFssp _repository;
        private readonly ICreateFileOfResult _createFile;
        private readonly object _personCollectionLock = new object();

        private readonly string _key;
        #endregion PrivateField

        #region PrivateMethod
        private static bool CheckResultStatus(EntityResultSearch res)
        {
            if (res.Code != 0) return false;

            var counts = res.Progress.Split(new string[] { " of " }, StringSplitOptions.RemoveEmptyEntries);

            return counts[0] == counts[1];
        }

        private RequestFound GetRequestFound(IRequestQuery query)
        {
            var req = new RequestFound(query);
            CollectionRequest.Add(req);

            return req;
        }

        private Task Found(Func<EntityResultSearch> entityResultSearch, RequestFound req)
        {
            return Task.Run(() =>
            {
                try
                {
                    var res = entityResultSearch();
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
            });
        }

        private void FoundGroopNumber(string fileName, IEnumerable<string> str)
        {
            var p = str.SplitCollection(50).ToList();

            int i = 1;

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = $"{fileName}_{i++}" });

                Found(() => _repository.SearchGroopNumber(item, _key), req);
            }
        }

        private void FoundGroopCompany(string fileName, IEnumerable<string> str)
        {
            var company = ServiceConvert.ConvertStringToEntityCompany(str);

            var p = company.SplitCollection(50);

            int i = 1;

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = $"{fileName}_{i++}" });

                Found(() => _repository.SearchGroopCompany(item, _key), req);
            }
        }

        private void FoundGroopPerson(string fileName, IEnumerable<string> str)
        {
            var persons = ServiceConvert.ConvertStringToEntityPerson(str);

            var p = persons.SplitCollection(50);

            int i = 1;

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = $"{fileName}_{i++}" });

                Found(() => _repository.SearchGroopPerson(item, _key), req);
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public async Task GetPerson(FoundPerson person)
        {
            var req = new RequestFound(person);
            CollectionRequest.Add(req);

            await Found(() => _repository.SearchPerson(ServiceConvert.ConvertFoundPersonToEntityPerson(person), _key), req).ConfigureAwait(false);
        }

        public async Task GetCompany(FoundCompany company)
        {
            var req = new RequestFound(company);
            CollectionRequest.Add(req);

            await Found(() => _repository.SearchCompany(ServiceConvert.ConvertFoundCompanyToEntityCompany(company), _key), req).ConfigureAwait(false);
        }

        public async Task GetNumber(string number)
        {
            var req = GetRequestFound(new FoundNumber() { Number = number });

            await Found(() => _repository.SearchIp(number, _key), req).ConfigureAwait(false);
        }

        public async Task<Result<RequestFound>> ProcessingList(string file)
        {
            Result<RequestFound> result = new Result<RequestFound>();

            return await Task.Run(async () =>
            {
                try
                {
                    var str = ServiceFile.ReadFile(file).Skip(1);

                    var t = await ServiceFile.GetTypeData(file).ConfigureAwait(false);
                    var fileName = ServiceFile.GetOnlyFileName(file);

                    if (t.Title == "Физические лица")
                    {
                        FoundGroopPerson(fileName, str);
                    }
                    else if (t.Title == "Юридические лица")
                    {
                        FoundGroopCompany(fileName, str);
                    }
                    else if (t.Title == "Номера исполнительных производств")
                    {
                        FoundGroopNumber(fileName, str);
                    }
                    else
                    {
                        throw new Exception("Неверный тип данных");
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                }

                return result;

            }).ConfigureAwait(false);
        }

        public void GetResult(RequestFound req)
        {
            int count = 60;
            string error = null;

            Task.Run(() =>
            {
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
            _createFile.OpenFolderFile(file);
        }
        #endregion PublicMethod
    }
}