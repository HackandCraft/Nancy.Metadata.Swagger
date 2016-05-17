using System;
using Nancy.Metadata.Swagger.SchemaGeneration;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.DemoApplication
{
    class NJsonSchemaGenerationStrategy : ISchemaGenerationStrategy
    {
        public object GenerateSchema(Type type)
        {
            return JsonSchema4.FromType(type);
        }
    }
}