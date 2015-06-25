using Newtonsoft.Json.Schema;

namespace Nancy.Metadata.Swagger.Core
{
    public class LicenseInstsaller
    {
        public static void SetJsonSchemaLicense(string licenseKey)
        {
            License.RegisterLicense(licenseKey);
        }
    }
}