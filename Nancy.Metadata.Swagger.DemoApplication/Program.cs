using System;
using Nancy.Hosting.Self;

namespace Nancy.Metadata.Swagger.DemoApplication
{
    public class Program
    {
        static void Main()
        {
            string url = "http://localhost:5000";

            var hostConfiguration = new HostConfiguration();
            hostConfiguration.UrlReservations.CreateAutomatically = true;
            NancyHost host = new NancyHost(new Uri(url), new DefaultNancyBootstrapper(), hostConfiguration);
            host.Start();

            Console.WriteLine("Nancy host is listening at {0}", url);
            Console.WriteLine("Press <Enter> to exit");

            Console.ReadLine();
        }
    }
}
