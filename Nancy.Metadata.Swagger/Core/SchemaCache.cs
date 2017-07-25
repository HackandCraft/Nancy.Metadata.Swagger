using System.Collections.Generic;

namespace Nancy.Metadata.Swagger.Core
{
    using Newtonsoft.Json.Linq;

    public static class SchemaCache
    {
         public static Dictionary<string, JObject> Cache = new Dictionary<string, JObject>();
    }
}