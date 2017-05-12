using System;
using Newtonsoft.Json.Schema.Generation;

namespace Nancy.Metadata.Swagger.Core
{
    public class NewtonsoftJsonSchemaGeneratorAdapter : IJsonSchemaGenerator
    {
        private readonly JSchemaGenerator jSchemaGenerator;

        public NewtonsoftJsonSchemaGeneratorAdapter(JSchemaGenerator jSchemaGenerator)
        {
            this.jSchemaGenerator = jSchemaGenerator;
        }

        public string GenerateSchema(Type modelType)
        {
            return jSchemaGenerator.Generate(modelType).ToString();
        }
    }
}
