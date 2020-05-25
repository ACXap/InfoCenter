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

            _key = settings.GetSettings().ApiKeyFssp;
        }

        private readonly IRepositoryFssp _repository;
        private readonly IConvertResultToFile _converter;
        private readonly IServiceFio _serviceFio;

        private readonly string _key;
        private readonly string _directoryLoadFile;

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
            });
        }

        private void GetResult(RequestFoundPerson req)
        {
            Task.Run(() =>
            {
                int count = 10;
                string error = null;

                while (count-- > 0)
                {
                    error = string.Empty;

                    try
                    {
                        Thread.Sleep(1000);
                        var res = _repository.Status(req.Token, _key);

                        if (string.IsNullOrEmpty(res.Exception))
                        {
                            if (res.Code == 0 && res.Progress == "1 of 1")
                            {
                                var result = _repository.Result(req.Token, _key);

                                if (string.IsNullOrEmpty(result.Exception))
                                {
                                    var str = _converter.ConvertResult(result.CollectionQuery[0].CollectionResult, req.FoundPerson.Fio);
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
                }

                req.ErrorRequest(error);
            });
        }

        public void GetPersonFile(string file)
        {
            System.Diagnostics.Process.Start("explorer", @"/select, " + file);
        }
    }
}