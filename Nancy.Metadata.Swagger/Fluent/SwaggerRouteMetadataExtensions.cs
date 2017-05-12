using System;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerRouteMetadataExtensions
    {
        public static SwaggerRouteMetadata With(this SwaggerRouteMetadata routeMetadata,
            Func<SwaggerEndpointInfo, SwaggerEndpointInfo> endpointInfoFactory)
        {
            routeMetadata.Info = endpointInfoFactory(routeMetadata.Info ?? new SwaggerEndpointInfo());

            return routeMetadata;
        }
    }
}
