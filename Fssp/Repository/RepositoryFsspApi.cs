using Common.Service;
using Fssp.Repository.Data;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Fssp.Repository
{
    public class RepositoryFsspApi : IRepositoryFssp
    {
        #region PrivateField
        private readonly string _urlService = "https://api-ip.fssprus.ru/api/v1.0";
        private readonly HttpService _httpService = new HttpService();
        #endregion PrivateField

        #region PublicMethod
        public EntityResultSearch SearchPerson(EntityPerson person, string key)
        {
            var url = $"search/physical?" +
                $"token={key}" +
                $"&region={person.Region}" +
                $"&firstname={person.Firstname}" +
                $"&lastname={person.Lastname}"
                + (string.IsNullOrEmpty(person.Secondname) ? "" : $"&secondname={person.Secondname}")
                + (string.IsNullOrEmpty(person.Birthdate) ? "" : $"&birthdate={person.Birthdate}");

            var str = _httpService.RequestGet(_urlService, url, HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponseSearch>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                TokenTask = res.Response.Task
            };

            return result;
        }
        public EntityResponsResult Result(string token, string key)
        {
            var url = $"result?token={key}&task={token}";

            var str = _httpService.RequestGet(_urlService, url, HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResponsResult()
            {
                Exception = res.Exception,
                Code = (int)res.Code,
                CollectionQuery = res.Response.Result.Select(x =>
                {
                    return new EntityQuery()
                    {
                        Type = (int)x.Query.Type,
                        Firstname = x.Query.Params.Firstname,
                        Lastname = x.Query.Params.Lastname,
                        Name = x.Query.Params.Name,
                        Number = x.Query.Params.Number,
                        Region = x.Query.Params.Region,
                        Status = x.Status,
                        CollectionResult = x.Result.Select(y =>
                        {
                            return new EntityResult()
                            {
                                Bailiff = y.Bailiff,
                                Department = y.Department,
                                Details = y.Details,
                                End = y.IpEnd,
                                Name = y.Name,
                                Production = y.ExeProduction,
                                Subject = y.Subject
                            };
                        }).ToList()
                    };
                }).ToList()
            };

            return result;
        }

        public string SearchCompany()
        {
            throw new NotImplementedException();
        }

        public string SearchGroop()
        {
            throw new NotImplementedException();
        }

        public string SearchIp()
        {
            throw new NotImplementedException();
        }
        #endregion PublicMethod

        public EntityStatus Status(string token, string key)
        {
            var url = $"status?token={key}&task={token}";

            var str = _httpService.RequestGet(_urlService, url, HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponseStatus>(str);

            var result = new EntityStatus()
            {
                Exception = res.Exception,
                Code = (int)res.Code,
                Progress = res.Response?.Progress
            };

            return result;
        }

        #region PrivateMethod

        #endregion PrivateMethod
    }
}