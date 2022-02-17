using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFirstSteps.Models
{
    public class Publication
    {
        [Required]
        public DateTime dateDePublication { get; set; }
        
        [Required]
        public string uneRessource { get; set; }
        public Ressource Ressource { get; set; }
        
        [Required]
        public string memberMail { get; set; }
        public Membre Membre { get; set; }
    }
}
