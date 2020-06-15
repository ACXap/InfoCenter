using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Common.Service
{
    public class HttpService
    {
        public HttpService(string url)
        {
            _url = url;
        }

        #region PrivateField
        private readonly string _url;
        #endregion PrivateField

        #region PrivateMethod
        private static HttpWebRequest GetRequest(string method, string url, EnumContentType contentType)
        {
            var request = WebRequest.CreateHttp(new Uri(url));
            request.ContentType = ConvertContentTypeToString(contentType);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Method = method;
            request.UserAgent = UserAgentService.GetUserAgent();
            request.Timeout = 60000;

            return request;
        }
        private static string GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
        private static string ConvertContentTypeToString(EnumContentType type)
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
                    return "application/x-www-form-urlencoded";
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public string RequestGet(string requestQuery, EnumContentType contentType)
        {
            HttpWebRequest request = GetRequest("GET", $"{_url}/{requestQuery}", contentType);

            return GetResponse(request);
        }
        public string RequestPost(string requestQuery, string requestBody, EnumContentType contentType)
        {
            HttpWebRequest request = GetRequest("POST", $"{_url}/{requestQuery}", contentType);

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestBody);
            }

            return GetResponse(request);
        }
        public string LoadFile(string requestQuery, string name)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile($"{_url}/{requestQuery}", name);
                return name;
            }
        }
        
        public string UrlEncode(string query)
        {
            return HttpUtility.UrlEncode(query);
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