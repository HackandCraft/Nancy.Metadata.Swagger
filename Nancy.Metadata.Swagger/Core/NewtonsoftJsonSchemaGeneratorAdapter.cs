using System;
using Newtonsoft.Json.Schema.Generation;

namespace Nancy.Metadata.Swagger.Core
{
    public class NewtonsoftJsonSchemaGeneratorAdapter : IJsonSchemaGenerator
    {
        private readonly JSchemaGenerator jsonSchemaGenerator;

        public NewtonsoftJsonSchemaGeneratorAdapter(JSchemaGenerator jsonSchemaGenerator)
        {
            this.jsonSchemaGenerator = jsonSchemaGenerator;
        }

        public string GenerateSchema(Type modelType)
        {
            return jsonSchemaGenerator.Generate(modelType).ToString();
        }
    }
}
