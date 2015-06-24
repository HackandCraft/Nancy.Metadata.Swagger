using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerResponseInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, SwaggerTypeDefinition> Headers { get; set; }

        [JsonProperty("schema")]
        public JSchema Schema { get; set; }
    }
}