using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerRouteMetadata
    {
        public SwaggerRouteMetadata(string path, string method)
        {
            Path = path;
            Method = method.ToLower();
        }

        public SwaggerRouteMetadata(RouteDescription desc) : this(desc.Path, desc.Method) { }

        public string Path { get; set; }

        public string Method { get; set; }

        public SwaggerEndpointInfo Info { get; set; }
    }
}