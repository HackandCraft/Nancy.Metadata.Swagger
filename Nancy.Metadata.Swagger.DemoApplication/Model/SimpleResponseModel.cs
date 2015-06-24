using System.ComponentModel.DataAnnotations;

namespace Nancy.Metadata.Swagger.DemoApplication.Model
{
    public class SimpleResponseModel
    {
        // without `Required` attribute JSchema will generate type ["string", "null"] with which swagger doesn't work 
        // and I don't know other way to fix it
        [Required]
        public string Hello { get; set; }
    }
}