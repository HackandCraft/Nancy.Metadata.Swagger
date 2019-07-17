using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;

namespace Nancy.Metadata.Swagger.Fluent
{
    public class SwaggerEndpointInfoBuilder
    {
        private readonly SwaggerEndpointInfo baseEndpointInfo;
        private readonly IJsonSchemaGenerator jsonSchemaGenerator;

        private Dictionary<string, SwaggerResponseInfo> responseInfos;
        private List<SwaggerRequestParameter> requestParameters;
        private string[] endpointTags;
        private string endpointDescription;
        private string endpointSummary;

        private SwaggerEndpointInfoBuilder(SwaggerEndpointInfo baseEndpointInfo, IJsonSchemaGenerator jsonSchemaGenerator)
        {
            this.baseEndpointInfo = baseEndpointInfo;
            this.jsonSchemaGenerator = jsonSchemaGenerator;
        }

        public static SwaggerEndpointInfoBuilder NewEndpointInfo()
        {
            return NewEndpointInfo(GetDefaultJsonSchemaGenerator());
        }

        public static SwaggerEndpointInfoBuilder NewEndpointInfo(IJsonSchemaGenerator jsonSchemaGenerator)
        {
            return NewEndpointInfo(new SwaggerEndpointInfo(), jsonSchemaGenerator);
        }

        public static SwaggerEndpointInfoBuilder NewEndpointInfo(SwaggerEndpointInfo baseEndpointInfo)
        {
            return NewEndpointInfo(baseEndpointInfo, GetDefaultJsonSchemaGenerator());
        }

        public static SwaggerEndpointInfoBuilder NewEndpointInfo(SwaggerEndpointInfo baseEndpointInfo, IJsonSchemaGenerator jsonSchemaGenerato)
        {
            return new SwaggerEndpointInfoBuilder(baseEndpointInfo, jsonSchemaGenerato);
        }

        public SwaggerEndpointInfoBuilder WithResponseModel(string statusCode, Type modelType, string description = null)
        {
            if (responseInfos == null)
            {
                responseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            responseInfos[statusCode] = GenerateResponseInfo(description, modelType);

            return this;
        }

        public SwaggerEndpointInfoBuilder WithDefaultResponse(Type responseType, string description = "Default response")
        {
            return WithResponseModel("200", responseType, description);
        }

        public SwaggerEndpointInfoBuilder WithResponse(string statusCode, string description)
        {
            if (responseInfos == null)
            {
                responseInfos = new Dictionary<string, SwaggerResponseInfo>();
            }

            responseInfos[statusCode] = GenerateResponseInfo(description);

            return this;
        }

        public SwaggerEndpointInfoBuilder WithRequestParameter(string name,
            string type = "string", string format = null, bool required = true, string description = null,
            string loc = "path")
        {
            if (requestParameters == null)
            {
                requestParameters = new List<SwaggerRequestParameter>();
            }

            requestParameters.Add(new SwaggerRequestParameter
            {
                Required = required,
                Description = description,
                Format = format,
                In = loc,
                Name = name,
                Type = type
            });

            return this;
        }

        public SwaggerEndpointInfoBuilder WithRequestModel(Type requestType, string name = "body", string description = null, bool required = true, string loc = "body")
        {
            if (requestParameters == null)
            {
                requestParameters = new List<SwaggerRequestParameter>();
            }

            requestParameters.Add(new SwaggerRequestParameter
            {
                Required = required,
                Description = description,
                In = loc,
                Name = name,
                Schema = new SchemaRef
                {
                    Ref = $"#/{SwaggerConstants.ModelDefinitionsKey}/{GetOrSaveSchemaReference(requestType)}"
                }
            });

            return this;
        }

        public SwaggerEndpointInfoBuilder WithDescription(string description, params string[] tags)
        {
            if (endpointTags == null)
            {
                if (tags.Length == 0)
                {
                    tags = new[] { "default" };
                }

                endpointTags = tags;
            }

            endpointDescription = description;

            return this;
        }

        public SwaggerEndpointInfoBuilder WithSummary(string summary)
        {
            endpointSummary = summary;
            return this;
        }

        public SwaggerEndpointInfo Build()
        {
            baseEndpointInfo.Summary = endpointSummary;
            baseEndpointInfo.Description = endpointDescription;
            baseEndpointInfo.Tags = endpointTags;
            if (responseInfos != null)
            {
                baseEndpointInfo.ResponseInfos = responseInfos;
            }

            if (requestParameters != null)
            {
                baseEndpointInfo.RequestParameters = requestParameters;
            }

            return baseEndpointInfo;
        }

        private SwaggerResponseInfo GenerateResponseInfo(string description, Type responseType)
        {
            return new SwaggerResponseInfo
            {
                Schema = new SchemaRef
                {
                    Ref = "#/definitions/" + GetOrSaveSchemaReference(responseType)
                },
                Description = description
            };
        }

        private SwaggerResponseInfo GenerateResponseInfo(string description)
        {
            return new SwaggerResponseInfo
            {
                Description = description
            };
        }

        private string GetOrSaveSchemaReference(Type type)
        {
            var key = type.FullName;

            if (SchemaCache.Cache.ContainsKey(key))
            {
                return key;
            }

            var schema = jsonSchemaGenerator.GenerateSchema(type);

            // I didn't find the way how to disallow JSchemaGenerator to use nullable types, swagger doesn't work with them
            var replaceNullableTypesPattern = @"\""type\"":[\s\n\r]*\[[\s\n\r]*\""(\w+)\"",[\s\n\r]*\""null\""[\s\n\r]*\]";
            var fixedNullableTypesSchema = Regex.Replace(schema, replaceNullableTypesPattern, "\"type\": \"$1\"");
            var fixedSchema = FixInnerDefinitionReferences(fixedNullableTypesSchema, key);
            SchemaCache.Cache[key] = JObject.Parse(fixedSchema);

            return key;
        }

        private string FixInnerDefinitionReferences(string jsonSchema, string parentDefinitionKey)
        {
            var jObject = JObject.Parse(jsonSchema);
            FixAllReferences(jObject, parentDefinitionKey);
            return jObject.ToString();
        }

        private void FixAllReferences(JObject jObject, string parentDefinitionKey)
        {
            foreach (var token in jObject)
            {
                if (token.Key.Equals(SwaggerConstants.SchemaReferenceKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    UpdateSchemaReference(parentDefinitionKey, token.Value);
                }
                else
                {
                    var tokenObject = token.Value as JObject;
                    if (tokenObject != null)
                    {
                        FixAllReferences(tokenObject, parentDefinitionKey);
                    }
                    var tokenArray = token.Value as JArray;
                    if (tokenArray != null)
                    {
                        foreach (var elementToken in tokenArray)
                        {
                            var elementObject = elementToken as JObject;
                            if (elementObject != null)
                            {
                                FixAllReferences(elementObject, parentDefinitionKey);
                            }
                        }
                    }
                }
            }
        }

        private static void UpdateSchemaReference(string parentDefinitionKey, JToken modelReferenceValue)
        {
            var currentReference = modelReferenceValue.ToString();
            var updatedReference = currentReference.Replace("#/", $"#/definitions/{parentDefinitionKey}/");
            modelReferenceValue.Replace(new JValue(updatedReference));
        }

        private static IJsonSchemaGenerator GetDefaultJsonSchemaGenerator()
        {
            return new NewtonsoftJsonSchemaGeneratorAdapter(
                    new JSchemaGenerator
                        {
                            SchemaIdGenerationHandling = SchemaIdGenerationHandling.FullTypeName,
                            SchemaReferenceHandling = SchemaReferenceHandling.None
                        });
        }
    }
}
