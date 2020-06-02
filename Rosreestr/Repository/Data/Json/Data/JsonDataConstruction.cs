using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataConstruction : JsonData
    {
        [JsonProperty("construction", NullValueHandling = NullValueHandling.Ignore)]
        public Construction Data { get; set; }
    }
}