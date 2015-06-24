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
            Get["SimpleRequest", "/hello"] = r => HelloWorld();
            Get["SimpleRequestWithParameter", "/hello/{name}"] = r => Hello(r.name);

            Post["SimplePostRequst", "/hello"] = r => HelloPost();
            Post["PostRequestWithModel", "/hello/model"] = r => HelloModel();
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
                .With(i => i.WithModelResponse("200", typeof(SimpleResponseModel), "Sample response"));

            Describe["SimpleRequestWithParameter"] = desc => new SwaggerRouteMetadata(desc)
                .With(i => i.WithModelResponse("200", typeof(SimpleResponseModel), "Sample response")
                            .WithRequestParameter("name"));

            Describe["SimplePostRequst"] = desc => new SwaggerRouteMetadata(desc)
                .With(info => info.WithModelResponse("200", typeof (SimpleResponseModel), "Sample response"));

            Describe["PostRequestWithModel"] = desc => new SwaggerRouteMetadata(desc)
                .With(info => info.WithModelResponse("200", typeof(SimpleResponseModel))
                    .WithResponse("400", "Bad request")
                    .WithRequestModel(typeof(SimpleRequestModel)));
        }
    }
}