using Newtonsoft.Json;
using Rosreestr.Repository.Data.Json.Address;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataParcel : JsonData
    {
        [JsonProperty("objectAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectAddress { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address.Address Address { get; set; }

        [JsonProperty("parcelData", NullValueHandling = NullValueHandling.Ignore)]
        public Parcel Data { get; set; }
    }
}