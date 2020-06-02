using Newtonsoft.Json;
using System;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonDataBuilding : JsonData
    {
        [JsonProperty("building", NullValueHandling = NullValueHandling.Ignore)]
        public Building Data { get; set; }
    }
}