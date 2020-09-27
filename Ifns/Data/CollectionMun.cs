using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ifns.Data
{
    public class CollectionMun
    {
        [JsonProperty("oktmmfList")]
        public Dictionary<string, string> OktmmfList { get; set; }
    }
}