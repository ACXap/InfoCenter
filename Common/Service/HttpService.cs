using System;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Service
{
    public class HttpService
    {
        #region PrivateField
        private string _userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";
        #endregion PrivateField

        #region PrivateMethod
        private HttpWebRequest GetRequest(string method, string url, EnumContentType contentType)
        {
            var request = WebRequest.CreateHttp(url);
            request.ContentType = ConvertContentTypeToString(contentType);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Method = method;
            request.UserAgent = _userAgent;
            request.Timeout = 10000;

            return request;
        }
        private string GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
        }
        private string ConvertContentTypeToString(EnumContentType type)
        {
            switch (type)
            {
                case EnumContentType.Html:
                    return "application/x-www-form-urlencoded; charset=UTF-8";
                case EnumContentType.Json:
                    return "application/json";
                case EnumContentType.Post:
                    return "application/x-www-form-urlencoded";
                default:
                    throw new ArgumentException();
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public string RequestGet(string url, string requestQuery, EnumContentType contentType)
        {
            HttpWebRequest request = GetRequest("GET", $"{url}/{requestQuery}", contentType);

            return GetResponse(request);
        }
        public string RequestPost(string url, string requestBody, EnumContentType contentType)
        {
            HttpWebRequest request = GetRequest("POST", url, contentType);

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
            }

            return GetResponse(request);
        }
        public string LoadFile(string url, string name)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(url, name);
                return name;
            }
        }
        #endregion PublicMethod

        public enum EnumContentType
        {
            Html,
            Json,
            Post
        }
    }
}