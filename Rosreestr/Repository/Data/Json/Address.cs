using Newtonsoft.Json;
using System;

namespace Rosreestr.Repository.Data.Json
{
    public class Address
    {
        [JsonProperty("tempId", NullValueHandling = NullValueHandling.Ignore)]
        public long? TempId { get; set; }

        [JsonProperty("objectId", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        [JsonProperty("regionKey", NullValueHandling = NullValueHandling.Ignore)]
        public long? RegionKey { get; set; }

        [JsonProperty("idzkoks", NullValueHandling = NullValueHandling.Ignore)]
        public long? Idzkoks { get; set; }

        [JsonProperty("regId", NullValueHandling = NullValueHandling.Ignore)]
        public long? RegId { get; set; }

        [JsonProperty("jobId", NullValueHandling = NullValueHandling.Ignore)]
        public long? JobId { get; set; }

        [JsonProperty("cadNum", NullValueHandling = NullValueHandling.Ignore)]
        public string CadNum { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty("cityName", NullValueHandling = NullValueHandling.Ignore)]
        public string CityName { get; set; }

        [JsonProperty("cityType", NullValueHandling = NullValueHandling.Ignore)]
        public string CityType { get; set; }

        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("streetType", NullValueHandling = NullValueHandling.Ignore)]
        public string StreetType { get; set; }

        [JsonProperty("level1Type", NullValueHandling = NullValueHandling.Ignore)]
        public string Level1Type { get; set; }

        [JsonProperty("level1Value", NullValueHandling = NullValueHandling.Ignore)]
        public string Level1Value { get; set; }

        [JsonProperty("codeKLADR", NullValueHandling = NullValueHandling.Ignore)]
        public string CodeKladr { get; set; }

        [JsonProperty("codeOKATO", NullValueHandling = NullValueHandling.Ignore)]
        public string CodeOkato { get; set; }

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

        [JsonProperty("mergedAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string MergedAddress { get; set; }

        [JsonProperty("ncadNum", NullValueHandling = NullValueHandling.Ignore)]
        public string NcadNum { get; set; }
        [JsonProperty("districtName", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictName { get; set; }

        [JsonProperty("districtType", NullValueHandling = NullValueHandling.Ignore)]
        public string DistrictType { get; set; }

        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        [JsonProperty("postalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }
        [JsonProperty("cadastralBlockId", NullValueHandling = NullValueHandling.Ignore)]
        public string CadastralBlockId { get; set; }

        [JsonProperty("localityName", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalityName { get; set; }

        [JsonProperty("localityType", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalityType { get; set; }

        [JsonProperty("ncadastralBlockId", NullValueHandling = NullValueHandling.Ignore)]
        public string NcadastralBlockId { get; set; }
    }
}