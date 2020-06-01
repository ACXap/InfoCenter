using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosreestr.Repository.Data.Json
{
	public class Flat
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

		[JsonProperty("cadNumKK", NullValueHandling = NullValueHandling.Ignore)]
		public string CadNumKk { get; set; }

		[JsonProperty("leter", NullValueHandling = NullValueHandling.Ignore)]
		public string Leter { get; set; }

		[JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty("assignationCode", NullValueHandling = NullValueHandling.Ignore)]
		public string AssignationCode { get; set; }

		[JsonProperty("totalAssets", NullValueHandling = NullValueHandling.Ignore)]
		public long? TotalAssets { get; set; }

		[JsonProperty("cadCostValue", NullValueHandling = NullValueHandling.Ignore)]
		public double? CadCostValue { get; set; }

		[JsonProperty("cadCostUnit", NullValueHandling = NullValueHandling.Ignore)]
		public long? CadCostUnit { get; set; }

		[JsonProperty("ccDateEntering", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateEntering { get; set; }

		[JsonProperty("ccDateValuation", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateValuation { get; set; }

		[JsonProperty("ccDateApproval", NullValueHandling = NullValueHandling.Ignore)]
		public DateTimeOffset? CcDateApproval { get; set; }

		[JsonProperty("area", NullValueHandling = NullValueHandling.Ignore)]
		public double? Area { get; set; }

		[JsonProperty("guidUl", NullValueHandling = NullValueHandling.Ignore)]
		public string GuidUl { get; set; }

		[JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
		public long? ParentId { get; set; }

		[JsonProperty("jsonOldNumbers", NullValueHandling = NullValueHandling.Ignore)]
		public string JsonOldNumbers { get; set; }

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

		[JsonProperty("oldNumbers", NullValueHandling = NullValueHandling.Ignore)]
		public List<OldNumber> OldNumbers { get; set; }

		[JsonProperty("ncadNumKK", NullValueHandling = NullValueHandling.Ignore)]
		public string NcadNumKk { get; set; }

		[JsonProperty("ncadNum", NullValueHandling = NullValueHandling.Ignore)]
		public string NcadNum { get; set; }
	}
}