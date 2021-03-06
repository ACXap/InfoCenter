﻿using Common.Service;
using Fssp.Repository.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Fssp.Repository
{
    public class RepositoryFsspApi : IRepositoryFssp
    {
        #region PrivateField
        private readonly HttpService _httpService = new HttpService("https://api-ip.fssprus.ru/api/v1.0");
        private readonly string _urlSearchGroop = "search/group";
        #endregion PrivateField

        #region PublicMethod
        public EntityResultSearch SearchPerson(EntityPerson person, string key)
        {
            var url = $"search/physical?" +
                $"token={key}" +
                $"&region={person?.Region}" +
                $"&firstname={person?.Firstname}" +
                $"&lastname={person?.Lastname}"
                + (string.IsNullOrEmpty(person.Secondname) ? "" : $"&secondname={person?.Secondname}")
                + (string.IsNullOrEmpty(person.Birthdate) ? "" : $"&birthdate={person?.Birthdate}");

            return GetResultSearch(url);
        }

        public EntityResultSearch SearchCompany(EntityCompany company, string key)
        {
            var url = $"search/legal?" +
                $"token={key}" +
                $"&region={company?.Region}" +
                $"&name={company?.Name}" +
                (string.IsNullOrEmpty(company?.Address) ? "" : $"&address={company?.Address}");

            return GetResultSearch(url);
        }

        public EntityResultSearch SearchIp(string number, string key)
        {
            var url = $"search/ip?token={key}&number={number}";

            return GetResultSearch(url);
        }

        public EntityResponsResult Result(string token, string key)
        {
            var url = $"result?token={key}&task={token}";

            var str = _httpService.RequestGet(url, HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResponsResult()
            {
                Exception = res.Exception,
                Code = res.Code,
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
                        Id = x.Query.Params.Id,
                        Status = x.Status,
                        Error = x.Result.FluffyResult?.Message,
                        CollectionResult = x.Result.PurpleResultArray?.Select(y =>
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

        public EntityResultSearch SearchGroopPerson(IEnumerable<EntityPerson> persons, string key)
        {
            var str = _httpService.RequestPost(_urlSearchGroop, GetRequestBodyPerson(persons, key), HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                TokenTask = res.Response.Task
            };

            return result;
        }

        public EntityResultSearch SearchGroopCompany(IEnumerable<EntityCompany> companys, string key)
        {
            var str = _httpService.RequestPost(_urlSearchGroop, GetRequestBodyCompany(companys, key), HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                TokenTask = res.Response.Task
            };

            return result;
        }

        public EntityResultSearch SearchGroopNumber(IEnumerable<EntityNumber> numbers, string key)
        {
            var str = _httpService.RequestPost(_urlSearchGroop, GetRequestBodyNumber(numbers, key), HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                TokenTask = res.Response.Task
            };

            return result;
        }

        public EntityResultSearch Status(string token, string key)
        {
            var url = $"status?token={key}&task={token}";

            var str = _httpService.RequestGet(url, HttpService.EnumContentType.Json);
            System.Diagnostics.Debug.WriteLine(str);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                Code = res.Code,
                Progress = res.Response?.Progress
            };

            return result;
        }
        #endregion PublicMethod

        #region PrivateMethod
        private EntityResultSearch GetResultSearch(string url)
        {
            var str = _httpService.RequestGet(url, HttpService.EnumContentType.Json);
            var res = JsonConvert.DeserializeObject<JsonResponsResult>(str);

            var result = new EntityResultSearch()
            {
                Exception = res.Exception,
                TokenTask = res.Response.Task
            };

            return result;
        }

        private static string GetRequestBodyPerson(IEnumerable<EntityPerson> persons, string key)
        {
            var b = new JsonGroopPerson()
            {
                Token = key,
                Request = persons.Select(x =>
                {
                    return new Request()
                    {
                        Type = 1,
                        Params = new Params()
                        {
                            Firstname = x.Firstname,
                            Lastname = x.Lastname,
                            Region = x.Region,
                            Secondname = x.Secondname,
                            Id = x.Id
                        }
                    };
                }).ToList()
            };

            return JsonConvert.SerializeObject(b);
        }

        private static string GetRequestBodyCompany(IEnumerable<EntityCompany> companys, string key)
        {
            var b = new JsonGroopPerson()
            {
                Token = key,
                Request = companys.Select(x =>
                {
                    return new Request()
                    {
                        Type = 2,
                        Params = new Params()
                        {
                            Name = x.Name,
                            Region = x.Region,
                            Address = x.Address,
                            Id = x.Id
                        }
                    };
                }).ToList()
            };

            return JsonConvert.SerializeObject(b);
        }

        private static string GetRequestBodyNumber(IEnumerable<EntityNumber> numbers, string key)
        {
            var b = new JsonGroopPerson()
            {
                Token = key,
                Request = numbers.Select(x =>
                {
                    return new Request()
                    {
                        Type = 3,
                        Params = new Params()
                        {
                            Number = x.Number,
                            Id = x.Id
                        }
                    };
                }).ToList()
            };

            return JsonConvert.SerializeObject(b);
        }
        #endregion PrivateMethod
    }
}