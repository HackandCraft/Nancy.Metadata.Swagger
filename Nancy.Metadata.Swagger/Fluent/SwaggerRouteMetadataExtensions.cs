using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.Model;
using System;

namespace Nancy.Metadata.Swagger.Fluent
{
    public static class SwaggerRouteMetadataExtensions
    {
        public static SwaggerRouteMetadata With(this SwaggerRouteMetadata routeMetadata,
            Func<SwaggerEndpointInfo, SwaggerEndpointInfo> info)
        {
            routeMetadata.Info = info(routeMetadata.Info ?? new SwaggerEndpointInfo(routeMetadata.Name));

            return routeMetadata;
        }
    }
}