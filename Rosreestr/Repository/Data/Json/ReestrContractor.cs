using Newtonsoft.Json;

namespace Rosreestr.Repository.Data.Json
{
    public class ReestrContractor
    {
        [JsonProperty("objectId")]
        public string ObjectId { get; set; }

        [JsonProperty("localId")]
        public string LocalId { get; set; }

        [JsonProperty("regionKey")]
        public string RegionKey { get; set; }

        [JsonProperty("rcType")]
        public string RcType { get; set; }

        [JsonProperty("jobId")]
        public string JobId { get; set; }

        [JsonProperty("guidUL")]
        public string GuidUl { get; set; }

        [JsonProperty("coName")]
        public string CoName { get; set; }

        [JsonProperty("schemeVersion")]
        public string SchemeVersion { get; set; }

        [JsonProperty("rsCode")]
        public string RsCode { get; set; }

        [JsonProperty("packageId")]
        public string PackageId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
}