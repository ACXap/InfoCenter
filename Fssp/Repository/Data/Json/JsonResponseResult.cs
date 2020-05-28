using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fssp.Repository.Data
{
    public class JsonResponsResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("task_start")]
        public string TaskStart { get; set; }

        [JsonProperty("task_end")]
        public string TaskEnd { get; set; }

        [JsonProperty("result")]
        public List<ResponseResult> Result { get; set; }
    }

    public partial class ResponseResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("result")]
        public List<ResultResult> Result { get; set; }
    }

    public class Query
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("params")]
        public Params Params { get; set; }
    }

    public class Params
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public int Region { get; set; }

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }
        [JsonProperty("secondname", NullValueHandling = NullValueHandling.Ignore)]
        public string Secondname { get; set; }
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public string Number { get; set; }
    }

    public class ResultResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("exe_production")]
        public string ExeProduction { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("bailiff")]
        public string Bailiff { get; set; }

        [JsonProperty("ip_end")]
        public string IpEnd { get; set; }
    }
}