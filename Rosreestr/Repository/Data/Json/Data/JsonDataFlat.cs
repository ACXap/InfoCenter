using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataFlat : JsonData
    {
        [JsonProperty("flat", NullValueHandling = NullValueHandling.Ignore)]
        public Flat Data { get; set; }
    }
}