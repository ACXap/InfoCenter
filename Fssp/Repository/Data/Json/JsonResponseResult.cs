using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fssp.Repository.Data
{
    public class JsonResponsResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
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
        [JsonProperty("task")]
        public string Task { get; set; }
        [JsonProperty("progress")]
        public string Progress { get; set; }
    }

    public partial class ResponseResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("result")]
        [JsonConverter(typeof(ResultUnionConverter))]
        public ResultUnion Result { get; set; }

    }
    public struct ResultUnion
    {
        public FluffyResult FluffyResult;
        public List<ResultResult> PurpleResultArray;
    }

    public class FluffyResult
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("errors")]
        public Errors Errors { get; set; }
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
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
    }

    public class Errors
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Name { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Region { get; set; }

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Firstname { get; set; }

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Lastname { get; set; }
        [JsonProperty("secondname", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Secondname { get; set; }
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Number { get; set; }
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

    internal class ResultUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ResultUnion) || t == typeof(ResultUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<FluffyResult>(reader);
                    return new ResultUnion { FluffyResult = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<ResultResult>>(reader);
                    return new ResultUnion { PurpleResultArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type ResultUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ResultUnion)untypedValue;
            if (value.PurpleResultArray != null)
            {
                serializer.Serialize(writer, value.PurpleResultArray);
                return;
            }
            if (value.FluffyResult != null)
            {
                serializer.Serialize(writer, value.FluffyResult);
                return;
            }
            throw new Exception("Cannot marshal type ResultUnion");
        }

        public static readonly ResultUnionConverter Singleton = new ResultUnionConverter();
    }
}