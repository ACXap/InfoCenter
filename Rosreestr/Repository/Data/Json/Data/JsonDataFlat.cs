using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataFlat : JsonData
    {
        [JsonProperty("objectAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectAddress { get; set; }

        [JsonProperty("reestrContractor", NullValueHandling = NullValueHandling.Ignore)]
        public ReestrContractor ReestrContractor { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address.Address Address { get; set; }

        [JsonProperty("flat", NullValueHandling = NullValueHandling.Ignore)]
        public Flat Data { get; set; }
    }
}