using Nancy.Metadata.Swagger.Model;
using Nancy.Routing;

namespace Nancy.Metadata.Swagger.Core
{
    public class SwaggerRouteMetadata
    {
        public SwaggerRouteMetadata(string path, string method, string name)
        {
            Path = path;
            Method = method.ToLower();
            Name = name;
        }

        public SwaggerRouteMetadata(RouteDescription desc) : this(desc.Path, desc.Method, desc.Name)
        {
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public string Name { get; set; }

        public SwaggerEndpointInfo Info { get; set; }
    }
}