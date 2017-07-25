using System;

namespace Nancy.Metadata.Swagger.Core
{
    public interface IJsonSchemaGenerator
    {
        string GenerateSchema(Type modelType);
    }
}
