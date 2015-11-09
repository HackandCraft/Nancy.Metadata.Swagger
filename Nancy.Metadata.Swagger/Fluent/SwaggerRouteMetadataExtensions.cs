using System;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerRouteMetadataExtensions
    {
        public static SwaggerRouteMetadata With(this SwaggerRouteMetadata routeMetadata,
            Func<SwaggerEndpointInfo, SwaggerEndpointInfo> info)
        {
            routeMetadata.Info = info(routeMetadata.Info ?? new SwaggerEndpointInfo());

            return routeMetadata;
        }
    }
}
