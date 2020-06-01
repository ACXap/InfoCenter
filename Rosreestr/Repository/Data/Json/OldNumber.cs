using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json
{
    public class OldNumber
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}