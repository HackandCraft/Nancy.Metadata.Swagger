namespace Nancy.Metadata.Swagger.SchemaGeneration
{
    public static class SchemaGenerator
    {
        public static ISchemaGenerationStrategy Strategy = new JsonNetSchemaGenerationStrategy();
    }
}