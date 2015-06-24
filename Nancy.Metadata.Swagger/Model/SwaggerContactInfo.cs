using Newtonsoft.Json;

namespace Nancy.Metadata.Swagger.Model
{
    public class SwaggerContactInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}