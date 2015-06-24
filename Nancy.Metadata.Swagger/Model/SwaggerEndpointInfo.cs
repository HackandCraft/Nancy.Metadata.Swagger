using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerEndpointInfo
    {
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
    }
}