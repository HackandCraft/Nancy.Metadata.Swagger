using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerTypeDefinition
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("required")]
        public List<string> RequiredProperties { get; set; }

        [JsonProperty(SwaggerConstants.TypePropertiesKey)]
        public Dictionary<string, SwaggerTypeDefinition> Properties { get; set; }
    }
}