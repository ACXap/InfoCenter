using Newtonsoft.Json;

namespace Fssp.Repository.Data
{
    internal class JsonResponseSearch
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("response")]
        public ResponseSearch Response { get; set; }
    }
    internal class ResponseSearch
    {
        [JsonProperty("task")]
        public string Task { get; set; }
    }
}