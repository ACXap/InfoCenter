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
}