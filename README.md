# Introduction

Nancy.Metadata.Swagger is a library that makes it easier to create API documentation for swagger (http://swagger.io/) with Nancy metadata modules.

## Dependencies

Nancy.Metadata.Swagger uses Json.NET (http://www.newtonsoft.com/json) to generate documentation and Json.NET Schema (http://www.newtonsoft.com/jsonschema) to generate objects schema (you may need license key for it if you want to generate objects schema)
Also it uses some of Nancy stuff, so it should be installed to.

# Gettings started

First you need to install Nancy.Metadata.Swagger and Nancy.Metadata.Modules nuget packages by:

	PM> Install-Package Nancy.Metadata.Swagger -Pre
	PM> Install-Package Nancy.Metadata.Modules

Now you can add metadata module and describe your methods:

Example module:

	public class RootModule : NancyModule
    {
        public RootModule() : base("/api")
        {
            Get["SimpleRequestWithParameter", "/hello/{name}"] = r => Hello(r.name);
        }
    }

Example metadata module (for ``%modulename%Module`` it should be named ``%modulename%MetadataModule``):
**IMPORTANT: Metadata module file should be placed in the same folder (namespace) with module**

	public class RootMetadataModule : MetadataModule<SwaggerRouteMetadata>
    {
        public RootMetadataModule()
        {
            Describe["SimpleRequestWithParameter"] = desc => new SwaggerRouteMetadata(desc)
                .With(i => i.WithResponseModel("200", typeof(SimpleResponseModel), "Sample response")
                            .WithRequestParameter("name"));
        }
    }

## Adding docs module

You also need to create one additional module that will return you json documentation. Here is the sample:

	public class DocsModule : SwaggerDocsModuleBase
    {
        public DocsModule(IRouteCacheProvider routeCacheProvider) 
        	: base(routeCacheProvider, 
        	  "/api/docs", 					// where module should be located
        	  "Sample API documentation",   // title
        	  "v1.0", 						// api version
        	  "localhost:5000",             // host
        	  "/api", 						// api base url (ie /dev, /api)
        	  "http")						// schemes
        {
        }
    }

## Adding swagger UI:

Now you are able to add swagger UI (you can download it from http://swagger.io/swagger-ui/) and point it to your docs module.
In index.html file you can set default url where ui should get json documentation file.

## Using a different Json Schema

Schema generation can be changed from the default strategy of using NewtonSoft.JsonSchema to any other type, by implementing `ISchemaGenerationStrategy`
and calling `Nancy.Metadata.Swagger.SchemaGeneration.SchemaGenerator.Use(ISchemaGenerationStrategy)`.

The following example describes using NJsonSchema for schema generation:

### Define the strategy
```
class NJsonSchemaGenerationStrategy : ISchemaGenerationStrategy
{
    public object GenerateSchema(Type type)
    {
        return JsonSchema4.FromType(type);
    }   
}
```

### Configure Nancy.Metadata.Swagger to use the strategy
```
SchemaGenerator.Use(new NJsonSchemaGenerationStrategy())
```
This should be done before Nancy configuration

## Configuring Json.NET schema license

Json.NET schema is limited with 10 schema generations per hour, so if you have more objects you need to configure license for it by:

	Nancy.Metadata.Swagger.Core.LicenseInstsaller.SetJsonSchemaLicense(Settings.Default.JsonSchemaLicenseKey);

It should be added before Nancy configuration.

# Additional information

Feel free to fork this library, create issues, pull requests and send me any feedback to a.kudryavcev@gmail.com

## Things to be done

There are lots of things that I want to implement here, but due to lack of time it's almost impossible. 
However, it would be nice to have:

1. Use free alternative for Json.NET schema or create our own
1. Use AOP way, so you'll be able to use attributes to describe your methods
1. Write code documentation
1. Add swagger UI to the package
1. Make it more simplier

If you have any ideas, please feel free to contact me
