using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Nancy.Metadata.Swagger.SchemaGeneration
{
    public class JsonNetSchemaGenerationStrategy : ISchemaGenerationStrategy
    {
        public object GenerateSchema(Type type)
        {
            JSchemaGenerator generator = new JSchemaGenerator
            {
                SchemaIdGenerationHandling = SchemaIdGenerationHandling.FullTypeName,
                SchemaReferenceHandling = SchemaReferenceHandling.None
            };

            JSchema schema = generator.Generate(type);

            // I didn't find the way how to disallow JSchemaGenerator to use nullable types, swagger doesn't work with them

            string tmp = schema.ToString();
            string s = @"\""type\"":[\s\n\r]*\[[\s\n\r]*\""(\w+)\"",[\s\n\r]*\""null\""[\s\n\r]*\]";
            tmp = Regex.Replace(tmp, s, "\"type\": \"$1\"");

            return JSchema.Parse(tmp);
        }
    }
}