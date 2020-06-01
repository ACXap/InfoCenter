using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json
{
    public class JsonObject
    {
        [JsonProperty("objectId", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        [JsonProperty("objectCn", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectCn { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("regionKey", NullValueHandling = NullValueHandling.Ignore)]
        public int RegionKey { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public int Source { get; set; }
    }
}