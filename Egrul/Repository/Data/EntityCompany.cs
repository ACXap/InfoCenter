using Newtonsoft.Json;

namespace Egrul.Repository.Data
{
    public class EntityCompany
    {
        [JsonProperty("g")]
        public string Director { get; set; }
        
        //[JsonProperty("cnt")]
        //public string CountRecords { get; set; }
       
        [JsonProperty("c")]
        public string Title { get; set; }
       
        [JsonProperty("a")]
        public string Address { get; set; }
        
        [JsonProperty("n")]
        public string FullTitle { get; set; }
        
        [JsonProperty("o")]
        public string Ogrn { get; set; }
       
        //[JsonProperty("k")]
        //public string k { get; set; }
        
        [JsonProperty("i")]
        public string Inn { get; set; }
        
        [JsonProperty("t")]
        public string TokenLoadFile { get; set; }
        
        //[JsonProperty("pg")]
        //public string Page { get; set; }
        
        //public string tot { get; set; }
        
        [JsonProperty("r")]
        public string DateOgrn { get; set; }
        
        [JsonProperty("p")]
        public string Kpp { get; set; }
        
        [JsonProperty("e")]
        public string DateRemove { get; set; }
    }
}