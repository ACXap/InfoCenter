using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Rosreestr.Repository.Data.Json
{
    public class Building
	{
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

		[JsonProperty("cadNum", NullValueHandling = NullValueHandling.Ignore)]
		public string CadNum { get; set; }

		[JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
		public string State { get; set; }

		[JsonProperty("dateCreated", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? DateCreated { get; set; }

		[JsonProperty("dateRemoved", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? DateRemoved { get; set; }

		[JsonProperty("cadNumParent", NullValueHandling = NullValueHandling.Ignore)]
		public string CadNumParent { get; set; }

		[JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
		public long? ParentId { get; set; }

		[JsonProperty("parentCadNum", NullValueHandling = NullValueHandling.Ignore)]
		public string ParentCadNum { get; set; }

		[JsonProperty("leter", NullValueHandling = NullValueHandling.Ignore)]
		public string Leter { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty("assignationCode", NullValueHandling = NullValueHandling.Ignore)]
		public string AssignationCode { get; set; }

		[JsonProperty("cadCostValue", NullValueHandling = NullValueHandling.Ignore)]
		public double? CadCostValue { get; set; }
		[JsonProperty("ccDateEntering", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateEntering { get; set; }

		[JsonProperty("ccDateValuation", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateValuation { get; set; }

		[JsonProperty("ccDateApproval", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateApproval { get; set; }

		[JsonProperty("explCharYearUsed", NullValueHandling = NullValueHandling.Ignore)]
		public long? ExplCharYearUsed { get; set; }

		[JsonProperty("explCharYearBuilt", NullValueHandling = NullValueHandling.Ignore)]
		public long? ExplCharYearBuilt { get; set; }

		[JsonProperty("floors", NullValueHandling = NullValueHandling.Ignore)]
		public string Floors { get; set; }

		[JsonProperty("undergroundFloors", NullValueHandling = NullValueHandling.Ignore)]
		public string UndergroundFloors { get; set; }

		[JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
		public double? Area { get; set; }

		[JsonProperty("contractorDate", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? ContractorDate { get; set; }

		[JsonProperty("guidUl", NullValueHandling = NullValueHandling.Ignore)]
		public string GuidUl { get; set; }

		[JsonProperty("jsonOldNumbers", NullValueHandling = NullValueHandling.Ignore)]
		public string JsonOldNumbers { get; set; }

		[JsonProperty("jsonElementsConstuct", NullValueHandling = NullValueHandling.Ignore)]
		public string JsonElementsConstuct { get; set; }

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

		[JsonProperty("rightsReg", NullValueHandling = NullValueHandling.Ignore)]
		public bool? RightsReg { get; set; }

		[JsonProperty("encumbrancesExists", NullValueHandling = NullValueHandling.Ignore)]
		public bool? EncumbrancesExists { get; set; }

		[JsonProperty("materials", NullValueHandling = NullValueHandling.Ignore)]
		public List<Material> Materials { get; set; }

		[JsonProperty("oldNumbers", NullValueHandling = NullValueHandling.Ignore)]
		public List<OldNumber> OldNumbers { get; set; }

		[JsonProperty("ncadNumParent", NullValueHandling = NullValueHandling.Ignore)]
		public string NcadNumParent { get; set; }

		[JsonProperty("ncadNum", NullValueHandling = NullValueHandling.Ignore)]
		public string NcadNum { get; set; }
	}
}