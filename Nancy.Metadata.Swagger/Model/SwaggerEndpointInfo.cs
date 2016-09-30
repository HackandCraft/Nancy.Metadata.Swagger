using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerEndpointInfo
    {
        public SwaggerEndpointInfo(string name)
        {
            OperationId = name;
        }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("responses")]
        public Dictionary<string, SwaggerResponseInfo> ResponseInfos { get; set; }

        [JsonProperty("parameters")]
        public List<SwaggerRequestParameter> RequestParameters { get; set; }

        [JsonProperty("operationId")]
        public string OperationId { get; set; }

        [JsonProperty("produces")]
        public string[] ContentType { get; set; }
    }
}