using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class PhotoVideo:Ressource
    {
        public string legende { get; set; }

        public long taille { get; set; }

        public string? path { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }
    }
}
