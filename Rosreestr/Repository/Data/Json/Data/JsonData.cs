using Newtonsoft.Json;
using System;

namespace Rosreestr.Repository.Data.Json.Data
{
    public class JsonData
    {
        [JsonProperty("dateRemoved", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateRemoved { get; set; }

        [JsonProperty("reestrContractor", NullValueHandling = NullValueHandling.Ignore)]
        public ReestrContractor ReestrContractor { get; set; }

        [JsonProperty("objectAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectAddress { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("objectId", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        [JsonProperty("tempId", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempId { get; set; }

        [JsonProperty("regionKey", NullValueHandling = NullValueHandling.Ignore)]
        public long? RegionKey { get; set; }

        [JsonProperty("idzkoks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Idzkoks { get; set; }

        [JsonProperty("regId", NullValueHandling = NullValueHandling.Ignore)]
        public long? RegId { get; set; }

        [JsonProperty("jobId", NullValueHandling = NullValueHandling.Ignore)]
        public long? JobId { get; set; }

        [JsonProperty("objectType", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectType { get; set; }

        [JsonProperty("objectCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectCode { get; set; }

        [JsonProperty("objectDesc", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectDesc { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("cadNum", NullValueHandling = NullValueHandling.Ignore)]
        public string CadNum { get; set; }

        [JsonProperty("cadastralBlockId", NullValueHandling = NullValueHandling.Ignore)]
        public string CadastralBlockId { get; set; }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId { get; set; }

        [JsonProperty("parentCadNum", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentCadNum { get; set; }

        [JsonProperty("dateCreated", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? DateCreated { get; set; }

        [JsonProperty("cadRecordDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CadRecordDate { get; set; }

        [JsonProperty("schemeVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string SchemeVersion { get; set; }

        [JsonProperty("rsCode", NullValueHandling = NullValueHandling.Ignore)]
        public string RsCode { get; set; }

        [JsonProperty("packageId", NullValueHandling = NullValueHandling.Ignore)]
        public string PackageId { get; set; }

        [JsonProperty("fileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("actualDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ActualDate { get; set; }

        [JsonProperty("ncadNum", NullValueHandling = NullValueHandling.Ignore)]
        public string NcadNum { get; set; }

        [JsonProperty("ncadastralBlockId", NullValueHandling = NullValueHandling.Ignore)]
        public string NcadastralBlockId { get; set; }
    }
}