using System;

namespace Nancy.Metadata.Swagger.SchemaGeneration
{
    public interface ISchemaGenerationStrategy
    {
        object GenerateSchema(Type type);
    }
}