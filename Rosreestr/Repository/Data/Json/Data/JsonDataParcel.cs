using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataParcel : JsonData
    {
        [JsonProperty("parcelData", NullValueHandling = NullValueHandling.Ignore)]
        public Parcel Data { get; set; }
    }
}