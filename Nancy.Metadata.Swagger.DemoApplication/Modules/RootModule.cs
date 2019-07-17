using Nancy.Metadata.Modules;
using Nancy.Metadata.Swagger.Core;
using Nancy.Metadata.Swagger.DemoApplication.Model;
using Nancy.Metadata.Swagger.Fluent;
using Nancy.ModelBinding;

namespace Nancy.Metadata.Swagger.DemoApplication.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule() : base("/api")
        {
            Get("/hello", r => HelloWorld(), name:"SimpleRequest");
            Get("/hello/{name}", r => Hello(r.name), name: "SimpleRequestWithParameter");

            Post("/hello", r => HelloPost(), name:"SimplePostRequest");
            Post("/hello/model", r => HelloModel(), name:"PostRequestWithModel");

            Post("/hello/nestedmodel", r => HelloNestedModel(), name: "PostRequestWithNestedModel");
        }

        private Response HelloNestedModel()
        {
            NestedRequestModel model = this.Bind<NestedRequestModel>();

            SimpleResponseModel response = new SimpleResponseModel
            {
                Hello = string.Format("Hello, {0}. We got your name from nested object", model.SimpleModel.Name)
            };

            return Response.AsJson(response);
        }

        private Response HelloModel()
        {
            SimpleRequestModel model = this.Bind<SimpleRequestModel>();

            SimpleResponseModel response = new SimpleResponseModel
            {
                Hello = string.Format("Hello, {0}", model.Name)
            };

            return Response.AsJson(response);
        }

        private Response HelloPost()
        {
            SimpleResponseModel response = new SimpleResponseModel
            {
                Hello = "Hello Post!"
            };

            return Response.AsJson(response);
        }

        private Response Hello(string name)
        {
            SimpleResponseModel response = new SimpleResponseModel
            {
                Hello = string.Format("Hello, {0}", name)
            };

            return Response.AsJson(response);
        }

        private Response HelloWorld()
        {
            SimpleResponseModel response = new SimpleResponseModel
            {
                Hello = "Hello World!"
            };

            return Response.AsJson(response);
        }
    }

    public class RootMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public RootMetadataModule()
        {
            Describe["SimpleRequest"] = desc => new SwaggerRouteMetadata(desc)
                .With(i => SwaggerEndpointInfoBuilder.NewEndpointInfo(i).WithResponseModel("200", typeof(SimpleResponseModel), "Sample response")
                            .WithSummary("Simple GET example").Build());

            Describe["SimpleRequestWithParameter"] = desc => new SwaggerRouteMetadata(desc)
                .With(i => SwaggerEndpointInfoBuilder.NewEndpointInfo(i).WithResponseModel("200", typeof(SimpleResponseModel), "Sample response")
                            .WithRequestParameter("name")
                            .WithSummary("Simple GET with parameters").Build());

            Describe["SimplePostRequest"] = desc => new SwaggerRouteMetadata(desc)
                .With(info => SwaggerEndpointInfoBuilder.NewEndpointInfo(info).WithResponseModel("200", typeof(SimpleResponseModel), "Sample response")
                    .WithSummary("Simple POST example").Build());

            Describe["PostRequestWithModel"] = desc => new SwaggerRouteMetadata(desc)
                .With(info => SwaggerEndpointInfoBuilder.NewEndpointInfo(info).WithResponseModel("200", typeof(SimpleResponseModel))
                    .WithResponse("400", "Bad request")
                    .WithSummary("Simple POST example with request model")
                    .WithRequestModel(typeof(SimpleRequestModel)).Build());

            Describe["PostRequestWithNestedModel"] = desc => new SwaggerRouteMetadata(desc)
                .With(info => SwaggerEndpointInfoBuilder.NewEndpointInfo(info).WithResponseModel("200", typeof(SimpleResponseModel))
                    .WithResponse("400", "Bad request")
                    .WithSummary("Simple POST example with nested request model")
                    .WithRequestModel(typeof(NestedRequestModel)).Build());
        }
    }
}