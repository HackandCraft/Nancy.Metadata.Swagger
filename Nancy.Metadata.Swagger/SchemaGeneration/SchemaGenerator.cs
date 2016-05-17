namespace Nancy.Metadata.Swagger.SchemaGeneration
{
    public static class SchemaGenerator
    {
        internal static ISchemaGenerationStrategy Strategy = new JsonNetSchemaGenerationStrategy();

        public static void Use(ISchemaGenerationStrategy strategy)
        {
            Strategy = strategy;
        }
    }
}