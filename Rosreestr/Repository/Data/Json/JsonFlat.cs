using Newtonsoft.Json;
using Rosreestr.Repository.Data.Json.Data;
using System.Linq;

namespace Rosreestr.Repository.Data.Json
{
    class JsonFlat : JsonObject, IRealEstate
    {
        [JsonProperty("objectData", NullValueHandling = NullValueHandling.Ignore)]
        public JsonDataFlat ObjectData { get; set; }

        public string KadastrNumber => ObjectData.NcadNum;

        public string Address => ObjectData.ObjectAddress;

        public string Area => ObjectData.Data?.Area?.ToString();

        public string Cost => ObjectData.Data?.CadCostValue?.ToString();

        public string CostDateEntering => ObjectData.Data?.CcDateEntering?.Date.ToShortDateString();

        public string CostDateValuation => ObjectData.Data?.CcDateValuation?.Date.ToShortDateString();

        public string CostDateApproval => ObjectData.Data?.CcDateApproval?.Date.ToShortDateString();

        public string ObjectDesc => ObjectData.ObjectDesc;

        public string Name => ObjectData.Name;

        public string ConditionalNumber => GetConditionalNumber();

        public string PreviouslyAssignedNumber => GetPreviouslyAssignedNumber();

        public string YearUsed => null;

        public string YearBuilt => null;

        private string GetPreviouslyAssignedNumber()
        {
            if (ObjectData.Data != null && ObjectData.Data.OldNumbers != null && ObjectData.Data.OldNumbers.Any())
            {
                return ObjectData.Data.OldNumbers.FirstOrDefault(x => x.Type == "01")?.Number;
            }

            return null;
        }

        private string GetConditionalNumber()
        {
            if (ObjectData.Data != null && ObjectData.Data.OldNumbers != null && ObjectData.Data.OldNumbers.Any())
            {
                return ObjectData.Data.OldNumbers.FirstOrDefault(x => x.Type == "02")?.Number;
            }

            return null;
        }
    }
}