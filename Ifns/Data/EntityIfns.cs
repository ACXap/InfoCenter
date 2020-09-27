using Newtonsoft.Json;

namespace Ifns.Data
{
    public class EntityIfns
    {
        [JsonProperty("ifnsDetails")]
        public IfnsDetails IfnsDetails { get; set; }

        [JsonProperty("sprouDetails")]
        public SproDetails SprouDetails { get; set; }

        [JsonProperty("sprofDetails")]
        public SproDetails SprofDetails { get; set; }

        [JsonProperty("form")]
        public Form Form { get; set; }

        [JsonProperty("payeeDetails")]
        public PayeeDetails PayeeDetails { get; set; }
    }

    public class Form
    {
        [JsonProperty("oktmmf")]
        public string Oktmmf { get; set; }

        [JsonProperty("ifns")]
        public string Ifns { get; set; }
    }

    public class IfnsDetails
    {
        private string _ifnsAddr;

        [JsonProperty("ifnsCode")]
        public string IfnsCode { get; set; }

        [JsonProperty("ifnsName")]
        public string IfnsName { get; set; }

        [JsonProperty("ifnsInn")]
        public string IfnsInn { get; set; }

        [JsonProperty("ifnsKpp")]
        public string IfnsKpp { get; set; }

        [JsonProperty("ifnsAddr")]
        public string IfnsAddr 
        {
            get => _ifnsAddr; 
            set
            {
                _ifnsAddr = value.Trim(new char[] { ',' });
            } 
        }

        [JsonProperty("ifnsPhone")]
        public string IfnsPhone { get; set; }

        [JsonProperty("ifnsComment")]
        public string IfnsComment { get; set; }

        [JsonProperty("sprof")]
        public string Sprof { get; set; }

        [JsonProperty("sprou")]
        public string Sprou { get; set; }
    }

    public class PayeeDetails
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("bankBic")]
        public string BankBic { get; set; }

        [JsonProperty("payeeAcc")]
        public string PayeeAcc { get; set; }

        [JsonProperty("payeeName")]
        public string PayeeName { get; set; }

        [JsonProperty("payeeInn")]
        public string PayeeInn { get; set; }

        [JsonProperty("payeeKpp")]
        public string PayeeKpp { get; set; }

        [JsonProperty("correspAcc")]
        public string CorrespAcc { get; set; }
    }

    public class SproDetails
    {
        [JsonProperty("ifnsCode")]
        public string IfnsCode { get; set; }

        [JsonProperty("sproCode")]
        public string SproCode { get; set; }

        [JsonProperty("sproName")]
        public string SproName { get; set; }

        [JsonProperty("sproAddr")]
        public string SproAddr { get; set; }

        [JsonProperty("sproPhone")]
        public string SproPhone { get; set; }

        [JsonProperty("sproComment")]
        public string SproComment { get; set; }
    }
}