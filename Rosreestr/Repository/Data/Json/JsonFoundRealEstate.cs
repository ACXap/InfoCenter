using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosreestr.Repository.Data.Json
{
    public class JsonFoundRealEstate
    {
        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("srcObject")]
        public int SrcObject { get; set; }

        [JsonProperty("regionKey")]
        public long RegionKey { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }

        [JsonProperty("objectCn")]
        public string ObjectCn { get; set; }

        [JsonProperty("objectCon")]
        public string ObjectCon { get; set; }

        [JsonProperty("subjectId")]
        public long SubjectId { get; set; }

        [JsonProperty("regionId")]
        public long RegionId { get; set; }

        [JsonProperty("settlementId")]
        public long SettlementId { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("house")]
        public string House { get; set; }

        [JsonProperty("addressNotes")]
        public string AddressNotes { get; set; }

        [JsonProperty("okato")]
        public string Okato { get; set; }

        [JsonProperty("apartment")]
        public string Apartment { get; set; }

        [JsonProperty("nobjectCn")]
        public string NobjectCn { get; set; }

        [JsonProperty("nobjectCon")]
        public string NobjectCon { get; set; }
    }
}