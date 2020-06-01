using Newtonsoft.Json;
using System;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataBuilding : JsonData
    {
        [JsonProperty("dateRemoved", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateRemoved { get; set; }

        [JsonProperty("reestrContractor", NullValueHandling = NullValueHandling.Ignore)]
        public ReestrContractor ReestrContractor { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address.Address Address { get; set; }

        [JsonProperty("building", NullValueHandling = NullValueHandling.Ignore)]
        public Building Data { get; set; }
    }
}