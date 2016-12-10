using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerSpecification
    {
        [JsonProperty("swagger")]
        public string SwaggerVersion { get { return "2.0"; } }

        [JsonProperty("info")]
        public SwaggerApiInfo ApiInfo { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("basePath")]
        public string BasePath { get; set; }

        [JsonProperty("schemes")]
        public string[] Schemes { get; set; }

        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, SwaggerEndpointInfo>> PathInfos { get; set; }

        [JsonProperty("definitions"), JsonConverter(typeof(Core.CustomJsonConverter))]
        public Dictionary<string, NJsonSchema.JsonSchema4> ModelDefinitions { get; set; }
    }
}