using Newtonsoft.Json;

namespace Fssp.Repository.Data
{
    internal class JsonResponseStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("response")]
        public ResponseStatus Response { get; set; }
    }
    internal partial class ResponseStatus
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("progress")]
        public string Progress { get; set; }
    }
}