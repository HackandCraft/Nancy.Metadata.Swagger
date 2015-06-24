using Nancy.Metadata.Swagger.Modules;
using Nancy.Routing;

namespace Nancy.Metadata.Swagger.DemoApplication.Modules
{
    public class DocsModule : SwaggerDocsModuleBase
    {
        public DocsModule(IRouteCacheProvider routeCacheProvider) : base(routeCacheProvider, "/api/docs", "Sample API documentation", "v1.0", "localhost:5000", "/api", "http")
        {
        }
    }
}