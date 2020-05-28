using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fssp.Repository.Data
{
    public class JsonGroopPerson
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("request")]
        public List<Request> Request { get; set; }
    }

    public class Request
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("params")]
        public Params Params { get; set; }
    }

    //public class Params
    //{
    //    public string Firstname { get; set; }
    //    public string Lastname { get; set; }
    //    public string Secondname { get; set; }
    //    public string Region { get; set; }
    //    public string Birthdate { get; set; }
    //}

}