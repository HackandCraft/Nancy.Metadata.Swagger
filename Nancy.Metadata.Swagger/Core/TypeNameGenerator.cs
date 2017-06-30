using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NJsonSchema;

namespace Nancy.Metadata.Swagger.Core
{
    public class TypeNameGenerator : NJsonSchema.ITypeNameGenerator, NJsonSchema.ISchemaNameGenerator
    {
        public string Generate(Type type)
        {
            return type.FullName;
        }

        public string Generate(NJsonSchema.JsonSchema4 schema, string typeNameHint)
        {
            return typeNameHint;
        }

        public string Generate(JsonSchema4 schema, string typeNameHint, ICollection<string> reservedTypeNames)
        {
            return typeNameHint;
        }
    }
}
