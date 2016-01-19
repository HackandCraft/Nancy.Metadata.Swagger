using System.Collections.Generic;
using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Core
{
    public static class SchemaCache
    {
         public static Dictionary<string, JSchema> Cache = new Dictionary<string, JSchema>();
    }
}