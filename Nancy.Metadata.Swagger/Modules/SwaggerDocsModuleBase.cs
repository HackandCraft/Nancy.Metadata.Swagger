using System.Collections.Generic;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Modules
{
    public abstract class SwaggerDocsModuleBase : NancyModule
    {
        private SwaggerSpecification swaggerSpecification;

        private readonly IRouteCacheProvider routeCacheProvider;
        private readonly string title;
        private readonly string apiVersion;
        private readonly string host;
        private readonly string apiBaseUrl;
        private readonly string[] schemes;

        protected SwaggerDocsModuleBase(IRouteCacheProvider routeCacheProvider, 
            string docsLocation = "/api/docs", 
            string title = "API documentation",
            string apiVersion = "1.0", 
            string host = "localhost:5000",
            string apiBaseUrl = "/", 
            params string[] schemes)
            : base(docsLocation)
        {
            this.routeCacheProvider = routeCacheProvider;
            this.title = title;
            this.apiVersion = apiVersion;
            this.host = host;
            this.apiBaseUrl = apiBaseUrl;
            this.schemes = schemes;

            Get["/"] = r => GetDocumentation();
        }

        public virtual Response GetDocumentation()
        {
            if (swaggerSpecification == null)
            {
                GenerateSpecification();
            }

            return Response.AsText(JsonConvert.SerializeObject(swaggerSpecification, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

        private void GenerateSpecification()
        {
            swaggerSpecification = new SwaggerSpecification
            {
                ApiInfo = new SwaggerApiInfo
                {
                    Title = title,
                    Version = apiVersion,
                },
                Host = host,
                BasePath = apiBaseUrl,
                Schemes = schemes,
            };

            // generate documentation
            IEnumerable<SwaggerRouteMetadata> metadata = routeCacheProvider.GetCache().RetrieveMetadata<SwaggerRouteMetadata>();

            Dictionary<string, Dictionary<string, SwaggerEndpointInfo>> endpoints = new Dictionary<string, Dictionary<string, SwaggerEndpointInfo>>();

            foreach (SwaggerRouteMetadata m in metadata)
            {
                if (m == null)
                {
                    continue;
                }

                string path = m.Path;
                
                if (!string.IsNullOrEmpty(swaggerSpecification.BasePath) && swaggerSpecification.BasePath != "/")
                {
                    path = path.Replace(swaggerSpecification.BasePath, "");
                }

                if (!endpoints.ContainsKey(path))
                {
                    endpoints[path] = new Dictionary<string, SwaggerEndpointInfo>();
                }

                endpoints[path].Add(m.Method, m.Info);

                // add definitions
                if (swaggerSpecification.ModelDefinitions == null)
                {
                    swaggerSpecification.ModelDefinitions = new Dictionary<string, NJsonSchema.JsonSchema4>();
                }

                foreach (string key in SchemaCache.Cache.Keys)
                {
                    if (swaggerSpecification.ModelDefinitions.ContainsKey(key))
                    {
                        continue;
                    }

                    swaggerSpecification.ModelDefinitions.Add(key, SchemaCache.Cache[key]);
                }
            }

            swaggerSpecification.PathInfos = endpoints;
        }
    }
}