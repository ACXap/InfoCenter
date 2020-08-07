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
using System.Linq;
using System.Runtime.CompilerServices;
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

        #region PrivateField
        private readonly IRepositoryFssp _repository;
        private readonly ICreateFileOfResult _createFile;
        private readonly ServiceFile<TypeDataFssp> _serviceFile = new ServiceFile<TypeDataFssp>();
        private readonly object _personCollectionLock = new object();

        private readonly string _key;

        private readonly int _secondPauseGetResult = 60;
        private readonly int _secondPauseRequest = 60;

        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<RequestFound> CollectionRequest { get; } = new ObservableCollection<RequestFound>();
        #endregion PublicProperties


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

        private async Task<EntityResponsResult> GetResult(RequestFound req, int pauseSecond = 5)
        {
            if (req == null || string.IsNullOrEmpty(req.Token)) return null;

            int count = 60;
            string error = null;
            EntityResponsResult result = null;

            return await Task.Run(() =>
             {
                 while (count-- > 0)
                 {
                     Thread.Sleep(pauseSecond * 1000);
                     error = string.Empty;
                     try
                     {
                         var res = _repository.Status(req.Token, _key);

                         if (string.IsNullOrEmpty(res.Exception) == false) error = res.Exception;
                         else
                         {
                             req.StatusRequest.Progress = res.Progress;

                             if (CheckResultStatus(res) == false) error = "Не выполнено";
                             else
                             {
                                 result = _repository.Result(req.Token, _key);

                                 if (string.IsNullOrEmpty(result.Exception) == false) error = result.Exception;
                                 else
                                 {
                                     error = null;
                                     break;
                                 }
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         error = ex.Message;
                     }
                 }

                 req.ErrorRequest(error);

                 return result;

             }).ConfigureAwait(false);
        }

        private static Task Found(Func<EntityResultSearch> entityResultSearch, RequestFound req)
        {
            return Task.Run(() =>
            {
                try
                {
                    var res = entityResultSearch();
                    if (string.IsNullOrEmpty(res.Exception))
                    {
                        req.Token = res.TokenTask;
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

        private async Task FoundGroopNumber(string fileName, IEnumerable<string> str)
        {
            var numbers = ServiceConvert.ConvertStringToEntityNumber(str);

            var p = numbers.SplitCollection(50).ToList();

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = fileName });

                await Task.Run(async () =>
                {
                    await Found(() => _repository.SearchGroopNumber(item, _key), req).ConfigureAwait(false);
                    var result = await GetResult(req, _secondPauseGetResult).ConfigureAwait(false);
                    await AppendSaveFile(req, result).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
        }

        private async Task FoundGroopCompany(string fileName, IEnumerable<string> str)
        {
            var company = ServiceConvert.ConvertStringToEntityCompany(str);

            var p = company.SplitCollection(50);

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = fileName });
                await Task.Run(async () =>
                {
                    await Found(() => _repository.SearchGroopCompany(item, _key), req).ConfigureAwait(false);
                    var result = await GetResult(req, _secondPauseGetResult).ConfigureAwait(false);
                    await AppendSaveFile(req, result).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
        }

        private async Task FoundGroopPerson(string fileName, IEnumerable<string> str)
        {
            var persons = ServiceConvert.ConvertStringToEntityPerson(str);

            await SaveErrorPerson(persons.Where(x => x.Id == "error").ToList(), fileName);

            var p = persons.Where(x => x.Id != "error").SplitCollection(50);

            foreach (var item in p)
            {
                var req = GetRequestFound(new FoundNumber() { Number = fileName });

                await Task.Run(async () =>
                {
                    await Found(() => _repository.SearchGroopPerson(item, _key), req).ConfigureAwait(false);
                    var result = await GetResult(req, _secondPauseGetResult).ConfigureAwait(false);
                    await AppendSaveFile(req, result).ConfigureAwait(false);
                }).ConfigureAwait(false);
            }
        }

        private async Task SaveErrorPerson(IEnumerable<EntityPerson> enumerable, string fileName)
        {
            if (!enumerable.Any()) return;

            var req = GetRequestFound(new FoundNumber() { Number = $"{fileName}_ошибки_{enumerable.Count()}" });
            var result = enumerable.Select(x =>
            {
                return x.Lastname;
            });

            await Task.Run(() =>
            {
                req.FileResult = _createFile.AppendCsv(result, req.Query.FirstField);
                req.StopRequest();
            });
        }

        private Task AppendSaveFile(RequestFound req, EntityResponsResult result)
        {
            return Task.Run(() =>
            {
                if (result != null)
                {
                    //req.FileResult = _createFile.AppendXlsx(ServiceConvert.ConvertEntityResultsToStrings(result), req.Query.FirstField);
                    req.FileResult = _createFile.AppendCsv(ServiceConvert.ConvertEntityResultsToStrings(result), req.Query.FirstField);
                    req.StopRequest();
                }
            });
        }

        private Task SaveFile(RequestFound req, EntityResponsResult result)
        {
            return Task.Run(() =>
            {
                if (result != null)
                {
                    if (result.CollectionQuery[0].CollectionResult.Any() == false) req.ErrorRequest("Ничего не найдено");
                    else
                    {
                        req.FileResult = _createFile.CreateXlsx(ServiceConvert.ConvertEntityResultsToStrings(result), req.Query.FirstField);
                        req.StopRequest();
                    }
                }
            });
        }
        #endregion PrivateMethod

        #region PublicMethod
        public async Task GetPerson(FoundPerson person)
        {
            var req = GetRequestFound(person);

            await Found(() => _repository.SearchPerson(ServiceConvert.ConvertFoundPersonToEntityPerson(person), _key), req).ConfigureAwait(false);
            var result = await GetResult(req).ConfigureAwait(false);
            await SaveFile(req, result).ConfigureAwait(false);
        }

        public async Task GetCompany(FoundCompany company)
        {
            var req = GetRequestFound(company);

            await Found(() => _repository.SearchCompany(ServiceConvert.ConvertFoundCompanyToEntityCompany(company), _key), req).ConfigureAwait(false);
            var result = await GetResult(req).ConfigureAwait(false);
            await SaveFile(req, result).ConfigureAwait(false);
        }

        public async Task GetNumber(string number)
        {
            var req = GetRequestFound(new FoundNumber() { Number = number });

            await Found(() => _repository.SearchIp(number, _key), req).ConfigureAwait(false);
            var result = await GetResult(req).ConfigureAwait(false);
            await SaveFile(req, result).ConfigureAwait(false);
        }

        public async Task<Result<RequestFound>> ProcessingList(string file)
        {
            Result<RequestFound> result = new Result<RequestFound>();

            return await Task.Run(async () =>
            {
                try
                {
                    var str = _serviceFile.ReadFile(file).Skip(1);

                    var t = await _serviceFile.GetTypeData(file).ConfigureAwait(false);
                    var fileName = CreateFileOffice.GetUniqueOnlyFileName(file);

                    if (t.Title == "Физические лица")
                    {
                        await FoundGroopPerson(fileName, str).ConfigureAwait(false);
                    }
                    else if (t.Title == "Юридические лица")
                    {
                        await FoundGroopCompany(fileName, str).ConfigureAwait(false);
                    }
                    else if (t.Title == "Номера исполнительных производств")
                    {
                        await FoundGroopNumber(fileName, str).ConfigureAwait(false);
                    }
                    else
                    {
                        throw new Exception("Неверный тип данных");
                    }
                }
                catch (FormatException fex)
                {
                    result.ErrorResult = new ErrorResult(fex.Message, EnumTypeError.ErrorFileFormat);
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                }

                return result;

            }).ConfigureAwait(false);
        }

        public void GetPersonFile(string file)
        {
            _createFile.OpenFolderFile(file);
        }
        #endregion PublicMethod
    }
}