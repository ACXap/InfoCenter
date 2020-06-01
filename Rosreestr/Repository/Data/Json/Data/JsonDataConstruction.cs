using Newtonsoft.Json;
using Rosreestr.Repository.Data.Json.Address;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataConstruction : JsonData
    {
        [JsonProperty("objectAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectAddress { get; set; }

        [JsonProperty("reestrContractor", NullValueHandling = NullValueHandling.Ignore)]
        public ReestrContractor ReestrContractor { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address.Address Address { get; set; }

        [JsonProperty("construction", NullValueHandling = NullValueHandling.Ignore)]
        public Construction Data { get; set; }
    }
}