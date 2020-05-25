﻿using Common.Service;
using Egrul.Repository.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Egrul.Repository
{
    public class RepositoryEgrulNalog : IRepositoryEgrul
    {
        #region PrivateField
        private readonly string _urlService = "https://egrul.nalog.ru";
        private readonly int _countMillisecond = 672;
        private readonly HttpService _httpService = new HttpService();
        #endregion PrivateField

        #region PrivateMethod
        private string GetStringUrlRequest(string token)
        {
            var time = GetTimeString();
            return $"search-result/{token}?r={time}&_{time}";
        }
        private string GetStringUrlRequestLoadFile(string token)
        {
            return $"vyp-download/{token}";
        }
        private string GetStringUrlRequestStatuse(string token)
        {
            var time = GetTimeString();
            return $"vyp-status/{token}?r={time}&_={time}";
        }
        private string GetStringUrlRequestSt(string token)
        {
            var time = GetTimeString();
            return $"vyp-request/{token}?r=&_={time}";
        }
        private string GetStringBodyRequestByData(string query)
        {
            return $"vyp3CaptchaToken=" +
                $"&page=" +
                $"&query={query}" +
                $"&region=" +
                $"&PreventChromeAutocomplete=";
        }
        private string GetTimeString()
        {
            var time = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return $"{time}{_countMillisecond}";
        }
        private bool CheckStatusFile(string token)
        {
           // отправляем запрос на подготовку файла
            _httpService.RequestGet(_urlService, GetStringUrlRequestSt(token), HttpService.EnumContentType.Json);

            int count = 10;
            while (count-- > 0)
            {
                Thread.Sleep(1000);

                // проверяем статус готовности файла
                var str = _httpService.RequestGet(_urlService, GetStringUrlRequestStatuse(token), HttpService.EnumContentType.Json);
                var r = JsonConvert.DeserializeObject<JsonResponseStatus>(str);

                if (r.status == "ready")
                {
                    return true;
                }
            }

            return false;
        }
        #endregion PrivateMethod

        #region PublicMethod
        public List<EntityCompany> GetCollectionCompany(string query)
        {
            // получаем токен списка предприятий
            var str = _httpService.RequestPost(_urlService, GetStringBodyRequestByData(query), HttpService.EnumContentType.Post);

            var token = JsonConvert.DeserializeObject<JsonResponse>(str);

            // получаем список предприятий по токену
            var str1 = _httpService.RequestGet(_urlService, GetStringUrlRequest(token.t), HttpService.EnumContentType.Json);

            var obj = JsonConvert.DeserializeObject<FoundCompany>(str1);

            return obj.rows;
        }

        public bool GetFile(string token, string fileName)
        {
            var ready = CheckStatusFile(token);
            if (ready)
            {
                // качаем файл
                var f = _httpService.LoadFile(_urlService+"/"+GetStringUrlRequestLoadFile(token), fileName);
                return !string.IsNullOrEmpty(f);
            }

            return false;
        }
        #endregion PublicMethod
    }
}