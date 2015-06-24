using System.ComponentModel.DataAnnotations;

namespace Nancy.Metadata.Swagger.DemoApplication.Model
{
    public class SimpleRequestModel
    {
        [Required]
        public string Name { get; set; } 
    }
}