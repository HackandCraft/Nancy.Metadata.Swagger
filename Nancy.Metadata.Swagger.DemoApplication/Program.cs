using System;
using Nancy.Hosting.Self;

namespace Nancy.Metadata.Swagger.DemoApplication
{
    public class Program
    {
        static void Main()
        {
            SchemaGeneration.SchemaGenerator.Use(new NJsonSchemaGenerationStrategy());

            string url = "http://localhost:5000";

            NancyHost host = new NancyHost(new Uri(url));
            host.Start();

            Console.WriteLine("Nancy host is listening at {0}", url);
            Console.WriteLine("Press <Enter> to exit");

            Console.ReadLine();
        }
    }
}
